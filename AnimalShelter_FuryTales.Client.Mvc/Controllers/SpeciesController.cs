using AnimalShelter_FuryTales.Client.Mvc.ViewModels;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Breeds;
using AnimalShelter_FuryTales.Consumer.Breeds.Models;
using AnimalShelter_FuryTales.Consumer.Species;
using AnimalShelter_FuryTales.Consumer.Species.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.Controllers;

public class SpeciesController : Controller{
private readonly ISpeciesApiService _speciesApiService;
private readonly IBreedApiService _breedApiService;
private readonly IFormBuilder _formBuilder;

public SpeciesController(ISpeciesApiService speciesApiService, IFormBuilder formBuilder, IBreedApiService breedApiService){
    _speciesApiService = speciesApiService;
    _formBuilder = formBuilder;
    _breedApiService = breedApiService;
}
    
   public async Task<IActionResult> Index(){
        var speciesFromApi = await _speciesApiService.GetSpeciesAsync();
       
        var speciesIndexViewModel = new SpeciesIndexViewModel{
           Species = new List<SpeciesItemViewModel>()
        };
        speciesIndexViewModel.Species = speciesFromApi
            .Select(species => new SpeciesItemViewModel{
            Id = species.Id,
            Name = species.Name,
            Breeds = species.Breeds.Select(breed => new BreedsItemViewModel{
                Id = breed.Id,
                Name = breed.Name
            })
          
        });
        return View(speciesIndexViewModel);
       
    }
    
    public async Task<IActionResult> Detail(Guid id){
        var specieFromApi = await _speciesApiService.GetSpeciesByIdAsync(id);
        var speciesItemModel = new SpeciesItemViewModel{
           Id = specieFromApi.Id,
           Name = specieFromApi.Name,
           Breeds = specieFromApi.Breeds.Select(breed => new BreedsItemViewModel{
               Id = breed.Id,
               Name = breed.Name
           }),
        };
      
        return View(speciesItemModel);
    }
    
    public async Task<IActionResult> AnimalsByBreedId(Guid id){
        var speciesFromApi = await _speciesApiService.GetSpeciesByIdAsync(id);
        var breedFromApi = await _breedApiService.GetBreedByIdAsync(id);
        var animalsFromApi = await _breedApiService.GetAnimalsByBreedIdAsync(breedFromApi.Id);
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

    [HttpGet]
    [Authorize(Policy = GlobalConstants.VolunteerRoleName)]
    public async Task<IActionResult> Create() {
        var speciesCreateViewModel = new SpeciesCreateViewModel();
        
        return View(speciesCreateViewModel);
    }

   
    [HttpPost]
    [Authorize(Policy = GlobalConstants.VolunteerRoleName)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SpeciesCreateViewModel speciesCreateViewModel){
        if (!ModelState.IsValid)
        {
            return View(speciesCreateViewModel);
        }

        var speciesToCreate = new SpeciesCreateRequestApiModel()
        {
            Name = speciesCreateViewModel.Name
            
        };

        await _speciesApiService.CreateSpeciesAsync(speciesToCreate, Request.Cookies["jwtToken"].ToString());
        TempData["success"] = "Species created successfully";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Policy = GlobalConstants.DonationsPolicy)]
    public async Task<IActionResult> Update(Guid id){
        var speciesToUpdate = await _speciesApiService.GetSpeciesByIdAsync(id);

        var speciesUpdateViewModel = new SpeciesUpdateViewModel(){
            Name = speciesToUpdate.Name,
            Id = speciesToUpdate.Id
        };
        return View(speciesUpdateViewModel);
    }

    
    [HttpPost]
    [Authorize(Policy = GlobalConstants.DonationsPolicy)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(SpeciesUpdateViewModel speciesUpdateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(speciesUpdateViewModel);
        }

        var speciesToUpdate = new SpeciesUpdateRequestApiModel()
        {
            Id = speciesUpdateViewModel.Id,
            Name = speciesUpdateViewModel.Name
        };

        await _speciesApiService.UpdateSpeciesAsync(speciesToUpdate, Request.Cookies["jwtToken"].ToString());
        TempData["success"] = "Species updated successfully";
        return RedirectToAction(nameof(Index));
    }
    
  

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Delete(Guid id){
       
        await _speciesApiService.DeleteSpeciesAsync(id, Request.Cookies[GlobalConstants.CookieToken]);      
        TempData["success"] = "Species deleted successfully";
        return RedirectToAction(nameof(Index));
    }
  
    
    
}