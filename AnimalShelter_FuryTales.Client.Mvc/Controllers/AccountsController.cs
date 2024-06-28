using System.Security.Claims;


using AnimalShelter_FuryTales.Consumer.Users;
using AnimalShelter_FuryTales.Consumer.Users.Models;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using AnimalShelter_FuryTales.Client.Mvc.ViewModels;
using AnimalShelter_FuryTales.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.Controllers;
[AllowAnonymous]
public class AccountsController : Controller{
    private readonly IUserAuthApiService _userAuthApiService;

    public AccountsController(IUserAuthApiService userAuthApiService)
    {
        _userAuthApiService = userAuthApiService;
    }

    public IActionResult Login(string returnUrl){
        var model = new AccountLoginViewModel
        {
            ReturnUrl = returnUrl
        };

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AccountLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");
        }

        var userToLogin = new UserLoginRequestApiModel
        {
            Username = model.Email,
            Password = model.Password
            
        };


        var result = await _userAuthApiService.LoginAsync(userToLogin);

        var token = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);
        var identity = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, CookieAuthenticationDefaults.AuthenticationScheme));

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity, authProperties);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        Response.Cookies.Append(GlobalConstants.CookieToken, result.Token, cookieOptions);

        if (!string.IsNullOrEmpty(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Response.Cookies.Delete(GlobalConstants.CookieToken);

        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register(string returnUrl){
        var model = new AccountRegisterViewModel();
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(AccountRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var newUser = new UserRegisterRequestApiModel
        {
            Username = model.Email,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword,
            FirstName = model.FirstName,
            LastName = model.LastName,
            RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd"),
        };
        

        var registrationResult = await _userAuthApiService.RegisterAsync(newUser);
        if(registrationResult == null)
        {
            return View(model);
        }
        var userToLogin = new UserLoginRequestApiModel
        {
            Username = model.Email,
            Password = model.Password
            
        };


        var result = await _userAuthApiService.LoginAsync(userToLogin);

        var token = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);
        var identity = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, CookieAuthenticationDefaults.AuthenticationScheme));

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity, authProperties);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        Response.Cookies.Append(GlobalConstants.CookieToken, result.Token, cookieOptions);

        if (!string.IsNullOrEmpty(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }


}
