
using AnimalShelter_FuryTales.Client.Mvc.ViewModels;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Animals.Models;
using AnimalShelter_FuryTales.Consumer.Breeds;
using AnimalShelter_FuryTales.Consumer.Breeds.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scala.StockSimulation.Utilities.Authorization;

namespace AnimalShelter_FuryTales.Client.Mvc.Controllers;

public class BreedsController : Controller{
    private readonly IBreedApiService _breedApiService;
    private readonly IFormBuilder _formBuilder;

    public BreedsController(IBreedApiService breedApiService, IFormBuilder formBuilder){
        _breedApiService = breedApiService;
        _formBuilder = formBuilder;
    }
    // GET
    
    [HttpGet]
    
    public async Task<IActionResult> Index(){
        var breedsFromApi = await _breedApiService.GetBreedsAsync();
       
        var breedsViewModel = new BreedsIndexViewModel{
           Breeds = new List<BreedsItemViewModel>()
        };
        breedsViewModel.Breeds = breedsFromApi.Select(breed => new BreedsItemViewModel{
            Id = breed.Id,
            Name = breed.Name
          
        });
        return View(breedsViewModel);
       
    }
    
    public async Task<IActionResult> Detail(Guid id){
        var breedFromApi = await _breedApiService.GetBreedByIdAsync(id);
        var breedsItemModel = new BreedsItemViewModel{
           Id = breedFromApi.Id,
           Name = breedFromApi.Name,
           SpeciesName = breedFromApi.Species,
        };
      
        return View(breedsItemModel);
    }
    
    public async Task<IActionResult> AnimalsByBreedId(Guid id){
        var animalsFromApi = await _breedApiService.GetAnimalsByBreedIdAsync(id);
        if (animalsFromApi.Length == 0){
            ViewBag.Message = "Currently, there are no animals available from this breed.";
        }
        var animalViewModel = new AnimalsIndexViewModel{
                Animals = new List<AnimalsItemViewModel>()
        };
        animalViewModel.Animals = animalsFromApi.Select(animal => new AnimalsItemViewModel{
            Id = animal.Id,
            Name = animal.Name,
            Image = animal.Image
        });
        return View("/Views/Animals/Index.cshtml",animalViewModel);
       
    }

    [Authorize(Policy = GlobalConstants.VolunteerRoleName)]
    public async Task<IActionResult> Create() {
        var model = new BreedsCreateViewModel();
        model.Species = await _formBuilder.GetSpeciesAsync();
        return View(model);
    }

   
    [HttpPost]
    [Authorize(Policy = GlobalConstants.VolunteerRoleName)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BreedsCreateViewModel model){
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var breedToCreate = new BreedCreateRequestApiModel()
        {
            Name = model.Name,
            SpeciesId = model.SpeciesId
        };

        await _breedApiService.CreateBreedAsync(breedToCreate, Request.Cookies[GlobalConstants.CookieToken]);
        TempData["success"] = "Breed created successfully";
        return RedirectToAction(nameof(Index));
    }

    [AuthorizeMultiplePolicy(GlobalConstants.VolunteerRoleName + "," + GlobalConstants.DonationsPolicy, false)]
    public async Task<IActionResult> Update(Guid id){
        var breedToUpdate = await _breedApiService.GetBreedByIdAsync(id);

        var breedsUpdateViewModel = new BreedsUpdateViewModel(){
            Id = breedToUpdate.Id,
            Name = breedToUpdate.Name,
            Species = await _formBuilder.GetSpeciesAsync()
        };
        return View(breedsUpdateViewModel);
    }

    
    [HttpPost]
    [AuthorizeMultiplePolicy(GlobalConstants.VolunteerRoleName + "," + GlobalConstants.DonationsPolicy, false)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(BreedsUpdateViewModel breedsUpdateViewModel)
    {
        if (!ModelState.IsValid)
        {
            breedsUpdateViewModel.Species = await _formBuilder.GetSpeciesAsync();
            return View(breedsUpdateViewModel);
        }

        var breedToUpdate = new BreedUpdateRequestApiModel()
        {
            Id = breedsUpdateViewModel.Id,
            Name = breedsUpdateViewModel.Name,
            SpeciesId = breedsUpdateViewModel.SpeciesId
        };

        await _breedApiService.UpdateBreedAsync(breedToUpdate, Request.Cookies[GlobalConstants.CookieToken]);
        TempData["success"] = "Breed updated successfully";
        return RedirectToAction(nameof(Index));
    }

    

    [HttpPost]
    [IgnoreAntiforgeryToken]
    [Authorize(Policy = GlobalConstants.VolunteerRoleName)]
    public async Task<IActionResult> Delete(Guid id){
        await _breedApiService.DeleteBreedAsync(id, Request.Cookies[GlobalConstants.CookieToken]);      
        TempData["success"] = "Breed deleted successfully";
        return RedirectToAction(nameof(Index));
    }
  
    
    
}