using System.Web;
using AnimalShelter_FuryTales.Api.Dtos.Users;
using AnimalShelter_FuryTales.Client.Mvc.Models;
using AnimalShelter_FuryTales.Client.Mvc.ViewModels;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Animals;
using AnimalShelter_FuryTales.Consumer.Animals.Models;
using AnimalShelter_FuryTales.Consumer.Users;
using AnimalShelter_FuryTales.Consumer.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scala.StockSimulation.Utilities.Authorization;

namespace AnimalShelter_FuryTales.Client.Mvc.Controllers;

public class UsersController : Controller{
    private readonly IUserApiService _userApiService;
    private readonly IAnimalApiService _animalApiService;
    private readonly IFormBuilder _formBuilder;

    public UsersController(IUserApiService userApiService, IFormBuilder formBuilder,
        IAnimalApiService animalApiService){
        _userApiService = userApiService;
        _formBuilder = formBuilder;
        _animalApiService = animalApiService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(){
        var usersFromApi = await _userApiService.GetUsersAsync();

        var usersIndexViewModelViewModel = new UsersIndexViewModel{
            Users = new List<UsersItemViewModel>()
        };
        usersIndexViewModelViewModel.Users = usersFromApi
            .Select(user => new UsersItemViewModel{
                Id = user.Id,
                FirstName = user.FirstName,
                ProfilePicture = user.ProfilePicture
            });
        return View(usersIndexViewModelViewModel);
    }
   
    [HttpGet]
    [AuthorizeMultiplePolicy(GlobalConstants.AdminRoleName + "," + GlobalConstants.VolunteerRoleName + "," + GlobalConstants.DonationsPolicy, false)]
    public async Task<IActionResult> IndexVolunteers(){
        var usersFromApi =  await _userApiService.GetVolunteersAsync(Request.Cookies[GlobalConstants.CookieToken]);

        var usersIndexViewModelViewModel = new UsersIndexViewModel{
            Users = new List<UsersItemViewModel>()
        };
        usersIndexViewModelViewModel.Users = usersFromApi
            .Select(user => new UsersItemViewModel{
                Id = user.Id,
                FirstName = user.FirstName,
                ProfilePicture = user.ProfilePicture
            });
        ViewBag.Index = GlobalConstants.IndexVolunteers;
        return View("Index",usersIndexViewModelViewModel);
    }

    [HttpGet]
    [Authorize(Policy = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> IndexAdopters(){
        var usersFromApi = await _userApiService.GetAdoptersAsync(Request.Cookies[GlobalConstants.CookieToken]);

        var usersIndexViewModelViewModel = new UsersIndexViewModel{
            Users = new List<UsersItemViewModel>()
        };
        usersIndexViewModelViewModel.Users = usersFromApi
            .Select(user => new UsersItemViewModel{
                Id = user.Id,
                FirstName = user.FirstName,
                ProfilePicture = user.ProfilePicture
            });
        ViewBag.Index = GlobalConstants.IndexAdopters;
        return View("Index",usersIndexViewModelViewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> Info(Guid id){
        var userFromApi = await _userApiService.GetUserByIdAsync(id);
        UsersItemViewModel usersItemViewModel = new UsersItemViewModel(){
            Id = userFromApi.Id,
            FirstName = userFromApi.FirstName,
            Ability = userFromApi.Ability,
            ProfilePicture = userFromApi.ProfilePicture,
            Animals = userFromApi.Animals?.Select(animal => new AnimalsItemViewModel{
                Id = animal.Id,
                Name = animal.Name
            }) ?? Enumerable.Empty<AnimalsItemViewModel>(),
            DonationItems = new List<DonationItemsInfoViewModel>()
        };
        //hoe kan ik de role van niet aangemelde user ophalen?
        
        if (string.IsNullOrEmpty(userFromApi.Ability)){
            ViewBag.TotalAmount = userFromApi.TotalAmount;
            ViewBag.Index = GlobalConstants.IndexAdopters;
        }
        else{
            ViewBag.Index = GlobalConstants.IndexVolunteers;
        }
        
        return View(usersItemViewModel);
    }

    [HttpGet]
    [Authorize(Policy = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Create(){
        UsersCreateViewModel usersCreateViewModel = new UsersCreateViewModel();
        usersCreateViewModel.Gender = await _formBuilder.GetGenderAsync();
        usersCreateViewModel.Animals = await _formBuilder.GetAnimalsAsync();
        return View(usersCreateViewModel);
    }

    [HttpPost]
    [Authorize(Policy = GlobalConstants.AdminRoleName)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UsersCreateViewModel usersCreateViewModel){
        if (!ModelState.IsValid){
            usersCreateViewModel.Gender = await _formBuilder.GetGenderAsync();
            usersCreateViewModel.Animals = await _formBuilder.GetAnimalsAsync();
            return View(usersCreateViewModel);
        }

        var userToCreate = new UsersCreateRequestApiModel(){
            FirstName = usersCreateViewModel.FirstName,
            LastName = usersCreateViewModel.LastName,
            Email = usersCreateViewModel.Email,
            Ability = usersCreateViewModel.Ability,
            Password = GlobalConstants.PasswordVolunteer,
            ConfirmPassword = GlobalConstants.PasswordVolunteer,
            ProfilePicture = ""
        };
        var checkedGender = usersCreateViewModel
            .Gender
            .Where(h => h.IsChecked)
            .Select(h => h.Text)
            .FirstOrDefault();
        userToCreate.Gender = checkedGender;

        userToCreate.Animals = new List<AnimalResponseApiModel>();
        if (usersCreateViewModel.AnimalsIds != null){
            var allAnimals = await _animalApiService.GetAnimalsAsync();
            var selectedAnimals = allAnimals
                .Where(u => usersCreateViewModel.AnimalsIds.Contains(u.Id))
                .Select(u => new AnimalResponseApiModel(){
                    Id = u.Id,
                    Name = u.Name
                })
                .ToList();
            userToCreate.Animals.AddRange(selectedAnimals);
        }
        else{
            userToCreate.Animals = new List<AnimalResponseApiModel>();
        }


        await _userApiService.CreateUsersAsync(userToCreate, Request.Cookies[GlobalConstants.CookieToken].ToString());
            
     
        TempData["success"] = "User created successfully";
        return RedirectToAction(nameof(IndexVolunteers));
    }

    [HttpGet]
    [Authorize(Policy = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Update(Guid id){
        var userToUpdate = await _userApiService.GetUserByIdAsync(id);
        if (userToUpdate == null){
            TempData["error"] = "User not found.";
            return RedirectToAction(nameof(Index));
        }

        var usersUpdateViewModel = new UsersUpdateViewModel{
            Id = userToUpdate.Id,
            FirstName = userToUpdate.FirstName,
            LastName = userToUpdate.LastName,
            Email = userToUpdate.Email,
            Ability = userToUpdate.Ability,
            ProfilePicture = userToUpdate.ProfilePicture,
            Gender = await _formBuilder.GetGenderAsync(),
            Animals = await _formBuilder.GetAnimalsAsync(),
        };

        foreach (var checkbox in usersUpdateViewModel.Gender){
            if (userToUpdate.Gender.Length == checkbox.Value){
                checkbox.IsChecked = true;
            }
        }

        return View(usersUpdateViewModel);
    }

    [HttpPost]
    [Authorize(Policy = GlobalConstants.AdminRoleName)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UsersUpdateViewModel usersUpdateViewModel){
        if (!ModelState.IsValid){
            usersUpdateViewModel.Gender = await _formBuilder.GetGenderAsync();
            usersUpdateViewModel.Animals = await _formBuilder.GetAnimalsAsync();
            
            return View(usersUpdateViewModel);
        }
        var userToUpdate = new UsersUpdateRequestApiModel(){
            Id = usersUpdateViewModel.Id,
            FirstName = usersUpdateViewModel.FirstName,
            LastName = usersUpdateViewModel.LastName,
            Email = usersUpdateViewModel.Email,

        };
        userToUpdate.Ability = usersUpdateViewModel.Ability ?? "";
        userToUpdate.Gender = usersUpdateViewModel
            .Gender
            .Where(h => h.IsChecked)
            .Select(h => h.Text)
            .FirstOrDefault();
        userToUpdate.Animals = new List<AnimalResponseApiModel>();
        if (usersUpdateViewModel.AnimalsIds != null){
            var allAnimals = await _animalApiService.GetAnimalsAsync();
            var selectedAnimals = allAnimals
                .Where(a => usersUpdateViewModel.AnimalsIds.Contains(a.Id))
                .Select(u => new AnimalResponseApiModel(){
                    Id = u.Id,
                    Name = u.Name
                })
                .ToList();
            userToUpdate.Animals = selectedAnimals;
        }

       
        var checkedGender = usersUpdateViewModel
            .Gender
            .Where(h => h.IsChecked)
            .Select(h => h.Text)
            .FirstOrDefault();
        userToUpdate.Gender = checkedGender;
      

        await _userApiService.UpdateUsersAsync(userToUpdate,Request.Cookies[GlobalConstants.CookieToken].ToString());

        TempData["success"] = "User updated successfully";
        return RedirectToAction(nameof(IndexVolunteers));
    }

    [HttpPost]
    [Authorize(Policy = GlobalConstants.AdminRoleName)]
    [IgnoreAntiforgeryToken] 
    public async Task<IActionResult> Delete(Guid id){
        var userToDelete = await _userApiService.GetUserByIdAsync(id);
        if (userToDelete == null){
            TempData["error"] = "User not found.";
            return RedirectToAction(nameof(Index));
        }
        bool isAdopter = string.IsNullOrEmpty(userToDelete.Ability);

        await _userApiService.DeleteUsersAsync(id, Request.Cookies[GlobalConstants.CookieToken]);
        TempData["success"] = "User deleted successfully";
        if (isAdopter){
           return RedirectToAction(nameof(IndexAdopters));
        }
        return RedirectToAction(nameof(IndexVolunteers));
       
    }
        
        
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(string token, Guid userId){
        var user = await _userApiService.GetUserByIdAsync(userId);
        var model = new UsersResetPasswordViewModel{
            Token = HttpUtility.UrlEncode(token),
            Id = userId,
            Email = user.Email,
            
        };

        return View(model);
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(UsersResetPasswordViewModel model){
        if (!ModelState.IsValid){
            return View(model);
        }

        var changePasswordModel = new UserChangePasswordApiModel{
            Id = model.Id,
            NewPassword = model.NewPassword,
            ConfirmPassword = model.ConfirmPassword,
        };
        model.Token = model.Token.Replace(" ", "");
        changePasswordModel.Token = model.Token;
        
            await _userApiService.ChangeUserPasswordAsync(changePasswordModel);
            return RedirectToAction("ResetPasswordConfirmation", "Users");
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
    


        
        

    
}