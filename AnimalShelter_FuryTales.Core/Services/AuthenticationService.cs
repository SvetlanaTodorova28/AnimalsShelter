using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;
using Microsoft.AspNetCore.Identity;


namespace AnimalShelter_FuryTales.Core.Services;

public class AuthenticationService:IAuthenticationService{
   
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

     public async Task<ResultModel<User>> RegisterUserAsync(User user, string password){
            var newUser = new User{
                UserName = user.UserName,
                Email = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.ProfilePicture,
                Ability = ""
            };
            
            var result = await _userManager.CreateAsync(newUser, password);
            await _userManager.AddToRoleAsync(newUser, GlobalConstants.AdopterRoleName);
            if (!result.Succeeded){
                return new ResultModel<User>{
                    Errors = new List<string>(result.Errors.Select(x => x.Description))
                };
            }
            //get the newUser to add claims
            newUser = await _userManager.FindByNameAsync(user.UserName);
            return new ResultModel<User>{
                Data = newUser
            };
        }

        public async Task<ResultModel<string>> Login(string username, string password){
            var user = await _userManager.FindByNameAsync(username);
            if (user == null){
                return new ResultModel<string>{
                    Errors = new List<string>{"Invalid username or password"}
                };
            }
            
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (!result.Succeeded)
            {
                return new ResultModel<string>
                {
                    Errors = new List<string>{"Invalid login attempt."}
                };
            }
           
            JwtSecurityToken token = await _tokenService.GenerateTokenAsync(user);
            string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResultModel<string>
            {
                Data = serializedToken
            };
        }
}