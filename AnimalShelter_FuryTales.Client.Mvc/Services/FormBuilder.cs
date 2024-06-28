

using AnimalShelter_FuryTales.Client.Mvc.Models;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Animals;
using AnimalShelter_FuryTales.Consumer.Breeds;
using AnimalShelter_FuryTales.Consumer.Species;
using AnimalShelter_FuryTales.Consumer.Enums;
using AnimalShelter_FuryTales.Consumer.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.Ocsp;


namespace AnimalShelter_FuryTales.Client.Mvc.Services;

public class FormBuilder : IFormBuilder{
    private readonly IAnimalApiService _animalApiService;
    private readonly IBreedApiService _breedApiService;
    private readonly ISpeciesApiService _speciesApiService;
    private readonly IEnumApiService _enumApiService;
    private readonly IUserApiService _userApiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    

    public FormBuilder(IAnimalApiService animalApiService, IBreedApiService breedApiService, 
        ISpeciesApiService speciesApiService,IEnumApiService enumApiService, IUserApiService userApiService,
        IHttpContextAccessor httpContextAccessor){
        _animalApiService = animalApiService;
        _breedApiService = breedApiService;
        _speciesApiService = speciesApiService;
        _enumApiService = enumApiService;
        _userApiService = userApiService;
        _httpContextAccessor = httpContextAccessor;
        
    }
    public async Task<List<CheckBoxModel>> GetGenderAsync(){
        var gender = await _enumApiService.GetGenderAsync();
        return gender
            .Select(item => new CheckBoxModel() {
                Value = item.Length,
                Text = item
            })
            .ToList();
    }

    public async Task<List<SelectListItem>> GetHealthAsync(){
        var health =  await _enumApiService.GetHealthAsync();
        return health
            .Select(item => new SelectListItem {
                Value = item,
                Text = item
            })
            .ToList();
    }


    public async Task<IEnumerable<SelectListItem>> GetBreedsAsync(){
        return  _breedApiService
            .GetBreedsAsync()
            .Result
            .Select(breed => new SelectListItem{
                Value = breed.Id.ToString(),
                Text = breed.Name
            });
    }

    public async Task<IEnumerable<SelectListItem>> GetSpeciesAsync(){
        return  
            _speciesApiService.GetSpeciesAsync()
            .Result
            .Select(species => new SelectListItem{
                Value = species.Id.ToString(),
                Text = species.Name
            });
    }
    public async Task<IEnumerable<SelectListItem>> GetUsersAsync(){
        var token = _httpContextAccessor.HttpContext.Request.Cookies[GlobalConstants.CookieToken];
        return _userApiService
            .GetVolunteersAsync(token)
            .Result
            .Where(user => user.Ability != null)
            .Select(user => new SelectListItem{
                Value = user.Id.ToString(),
                Text = user.FirstName
            });

    }
    public async Task<IEnumerable<SelectListItem>> GetAnimalsAsync(){
        return _animalApiService
            .GetAnimalsAsync()
            .Result
            .Select(animal => new SelectListItem{
                Value = animal.Id.ToString(),
                Text = animal.Name
            });

    }
    

   
}