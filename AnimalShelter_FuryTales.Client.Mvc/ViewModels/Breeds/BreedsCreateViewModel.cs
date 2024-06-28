using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class BreedsCreateViewModel{
    
    public string? Name { get; set; }
    public IEnumerable<SelectListItem>? Species { get; set; }
    [Display(Name = "Species")]
    public Guid SpeciesId { get; set; }
}