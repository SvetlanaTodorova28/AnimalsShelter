
using AnimalShelter_FuryTales.Client.Mvc.ViewModels;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Animals;
using AnimalShelter_FuryTales.Consumer.Animals.Models;
using AnimalShelter_FuryTales.Consumer.Breeds;
using AnimalShelter_FuryTales.Consumer.Breeds.Models;
using AnimalShelter_FuryTales.Consumer.Enums;
using AnimalShelter_FuryTales.Consumer.Species;
using AnimalShelter_FuryTales.Consumer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.Controllers;

public class AnimalsController : Controller{
    private readonly IAnimalApiService _animalApiService;
    private readonly IBreedApiService _breedApiService;
    private readonly ISpeciesApiService _speciesApiService;
    private readonly IUserApiService _userApiService;
    private readonly IFormBuilder _formBuilder;
    private readonly IEnumApiService _enumApiService;

    public AnimalsController(IAnimalApiService animalApiService, IBreedApiService breedApiService, IFormBuilder formBuilder,
        IUserApiService userApiService, IEnumApiService enumApiService, ISpeciesApiService speciesApiService){
        _animalApiService = animalApiService;
        _breedApiService = breedApiService;
        _formBuilder = formBuilder;
        _userApiService = userApiService;
        _enumApiService = enumApiService;
        _speciesApiService = speciesApiService;
    }
    // GET
    
    public async Task<IActionResult> Index(){
        var animalsFromApi = await _animalApiService.GetAnimalsAsync();
       
        var animalsIndexViewModel = new AnimalsIndexViewModel(){
            Animals = new List<AnimalsItemViewModel>()
        };
        animalsIndexViewModel.Animals = animalsFromApi
            .Select(animal => new AnimalsItemViewModel(){
            Id = animal.Id,
            Name = animal.Name,
            Image = animal.Image,
            Breed = animal.Breed,
            Species = animal.Species,
            Health = animal.Health,
            Age = animal.Age,
            Gender = animal.Gender,
            Description = animal.Description,
            Donation = animal.MonthlyFoodExpenses
        });
        
        return View(animalsIndexViewModel);
    }

    public async Task<IActionResult> Info(Guid id){
        var animalFromApi = await _animalApiService.GetAnimalByIdAsync(id);
        var breedFromApi = await _breedApiService.GetBreedByIdAsync(animalFromApi.Breed.Id);
        var speciesFromApi = await _speciesApiService.GetSpeciesByIdAsync(animalFromApi.Species.Id);
      
        
        AnimalsItemViewModel animalsItemViewModel = new AnimalsItemViewModel(){
            Id = animalFromApi.Id,
            Name = animalFromApi.Name,
            Image = animalFromApi.Image,
            Breed = new BreedResponseApiModel(){
                Name = breedFromApi.Name
            },
            Species = new SpeciesResponseApiModel(){
                Name = speciesFromApi.Name
            },
            Health = animalFromApi.Health,
            Age = animalFromApi.Age,
            Gender = animalFromApi.Gender,
            Description = animalFromApi.Description,
            Users =  animalFromApi.Users.Select(user => new BaseViewModel(){
            Id = user.Id,
            Name = user.FirstName}),
            Donation = animalFromApi.MonthlyFoodExpenses
        };
        return View(animalsItemViewModel);
    }
    
    [HttpGet]
    [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
    public async Task<IActionResult> Create(){
            var animalsCreateViewModel = new AnimalsCreateViewModel();
            animalsCreateViewModel.Species = await _formBuilder.GetSpeciesAsync();
            animalsCreateViewModel.Breeds = await _formBuilder.GetBreedsAsync();
            animalsCreateViewModel.Gender = await _formBuilder.GetGenderAsync();
            animalsCreateViewModel.Health = await _formBuilder.GetHealthAsync();
            animalsCreateViewModel.Users = await _formBuilder.GetUsersAsync();
            return View(animalsCreateViewModel);
    }
    
   
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
    public async Task<IActionResult> Create(AnimalsCreateViewModel animalsCreateViewModel){
        if (!ModelState.IsValid){
            animalsCreateViewModel.Species = await _formBuilder.GetSpeciesAsync();
            animalsCreateViewModel.Breeds = await _formBuilder.GetBreedsAsync();
            animalsCreateViewModel.Gender = await _formBuilder.GetGenderAsync();
            animalsCreateViewModel.Health = await _formBuilder.GetHealthAsync();
            animalsCreateViewModel.Users = await _formBuilder.GetUsersAsync();
            return View(animalsCreateViewModel);
        }
        
        var animalToCreate = new AnimalCreateRequestApiModel(){
            Name = animalsCreateViewModel.Name,
            BreedId = animalsCreateViewModel.BreedId,
            SpeciesId = animalsCreateViewModel.SpeciesId,
            Age = animalsCreateViewModel.Age,
            Description = animalsCreateViewModel.Description,
        };

        if (animalsCreateViewModel.Image != null){
            animalToCreate.Image =animalsCreateViewModel.Image;
        }
        animalToCreate.Health = animalsCreateViewModel.HealthId;

        
        var checkedGender = animalsCreateViewModel
            .Gender
            .Where(h => h.IsChecked)
            .Select(h => h.Text)
            .FirstOrDefault();
        animalToCreate.Gender = checkedGender;
        
        animalToCreate.Donation = animalsCreateViewModel.Donation;

        var allUsers = await _userApiService.GetVolunteersAsync(Request.Cookies[GlobalConstants.CookieToken]);
    var selectedUserIds = allUsers
    .Where(u => animalsCreateViewModel.UsersIds.Contains(u.Id))
    .Select(u => u.Id.ToString())  
    .ToList();

animalToCreate.UsersIds = selectedUserIds;

        await _animalApiService.CreateAnimalAsync(animalToCreate, Request.Cookies[GlobalConstants.CookieToken].ToString());
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
    public async Task<IActionResult> Update(Guid id){
        var animalToUpdate = await _animalApiService.GetAnimalByIdAsync(id);
        if (animalToUpdate == null){
            TempData["error"] = "Animal not found.";
            return RedirectToAction(nameof(Index));
        }

        var animalUpdateViewModel = new AnimalsUpdateViewModel(){
            Id = animalToUpdate.Id,
            Name = animalToUpdate.Name,
            Age = animalToUpdate.Age,
            Donation = animalToUpdate.MonthlyFoodExpenses,
            Description = animalToUpdate.Description,
            Gender = await _formBuilder.GetGenderAsync(),
            Species = await _formBuilder.GetSpeciesAsync(),
          
            Breeds = await _formBuilder.GetBreedsAsync(),
            BreedId =  animalToUpdate.Breed.Id,
            Health = await _formBuilder.GetHealthAsync(),
            HealthId = animalToUpdate.Health,
            Users = await _formBuilder.GetUsersAsync(),
            UsersIds = animalToUpdate.Users.Select(u => u.Id).ToList(),
            ExistingImage = animalToUpdate.Image
            
        };

        animalUpdateViewModel.SpeciesId = animalToUpdate.Species.Id;
        foreach (var checkbox in animalUpdateViewModel.Gender){
            if (animalToUpdate.Gender.Length == checkbox.Value){
                checkbox.IsChecked = true;
            }
        }

        return View(animalUpdateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
    public async Task<IActionResult> Update(AnimalsUpdateViewModel animalsUpdateViewModel){
        if (!ModelState.IsValid){
            animalsUpdateViewModel.Gender = await _formBuilder.GetGenderAsync();
            animalsUpdateViewModel.Species = await _formBuilder.GetSpeciesAsync();
            animalsUpdateViewModel.Breeds = await _formBuilder.GetBreedsAsync();
            animalsUpdateViewModel.Health = await _formBuilder.GetHealthAsync();
            animalsUpdateViewModel.Users = await _formBuilder.GetUsersAsync();
            
            return View(animalsUpdateViewModel);
        }
        var animalToUpdate = new AnimalUpdateRequestApiModel(){
            Id = animalsUpdateViewModel.Id,
            Name = animalsUpdateViewModel.Name,
            BreedId = animalsUpdateViewModel.BreedId,
            SpeciesId = animalsUpdateViewModel.SpeciesId,
            Age = animalsUpdateViewModel.Age,
            Description = animalsUpdateViewModel.Description,
            Health = animalsUpdateViewModel.HealthId,
            Donation = animalsUpdateViewModel.Donation,
        };
        
        var allUsers = await _userApiService.GetVolunteersAsync(Request.Cookies[GlobalConstants.CookieToken]);
        if (animalsUpdateViewModel.UsersIds != null && animalsUpdateViewModel.UsersIds.Any()){
            var selectedUserIds = allUsers
                .Where(u => animalsUpdateViewModel.UsersIds.Contains(u.Id))
                .Select(u => u.Id.ToString())
                .ToList();
            animalToUpdate.UsersIds = selectedUserIds;
        }
        
        if (animalsUpdateViewModel.NewImage != null){
            animalToUpdate.Image =animalsUpdateViewModel.NewImage;
        }
        
        var checkedGender = animalsUpdateViewModel
            .Gender
            .Where(h => h.IsChecked)
            .Select(h => h.Text)
            .FirstOrDefault();
        animalToUpdate.Gender = checkedGender;
      

        await _animalApiService.UpdateAnimalAsync(animalToUpdate,Request.Cookies[GlobalConstants.CookieToken].ToString());

        TempData["success"] = "Animal updated successfully";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
    public async Task<IActionResult> Delete(Guid id){
        await _animalApiService.DeleteAnimalAsync(id, Request.Cookies[GlobalConstants.CookieToken]);
        TempData["success"] = "Animal deleted successfully";
        return RedirectToAction(nameof(Index));
    }
}