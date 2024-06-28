using AnimalShelter_FuryTales.Client.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalShelter_FuryTales.Client.Mvc;

public interface IFormBuilder{
    public Task<IEnumerable<SelectListItem>> GetBreedsAsync();
    public Task<IEnumerable<SelectListItem>> GetSpeciesAsync();
    public Task<IEnumerable<SelectListItem>> GetAnimalsAsync();
    public Task<List<SelectListItem>> GetHealthAsync();
    public Task<List<CheckBoxModel>> GetGenderAsync();
    public Task<IEnumerable<SelectListItem>> GetUsersAsync();
    
    
    
}