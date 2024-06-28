using System.ComponentModel.DataAnnotations;
using AnimalShelter_FuryTales.Client.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class AnimalsCreateViewModel{
   
    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; set; }
    public IEnumerable<SelectListItem>? Species { get; set; }
    [Display(Name = "Species")]
    [Required(ErrorMessage = "{0} is required")]
    public Guid SpeciesId { get; set; }
    
    public IEnumerable<SelectListItem>? Breeds { get; set; }
    [Display(Name = "Breed")]
    [Required(ErrorMessage = "{0} is required")]
    public Guid BreedId { get; set; }
    
    public IEnumerable<SelectListItem>? Users { get; set; }
    [Display(Name = "Volunteers : ")]
    public IEnumerable<Guid>? UsersIds { get; set; } = new List<Guid>();
   
    
    public int? Age { get; set; }
    
    
    public List<CheckBoxModel>? Gender { get; set; }
    [Display(Name = "Gender")]
    [Required(ErrorMessage = "{0} is required")]
    public int GenderId { get; set; }
    
    
    public IEnumerable<SelectListItem>? Health { get; set; }
    [Display(Name = "Health")]
    [Required(ErrorMessage = "{0} is required")]
    public string HealthId { get; set; }

    
    
    public string? Description { get; set; }
    
    [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Please enter a valid number with up to two decimal places using a point as the decimal separator.")]
    public decimal? Donation { get; set; }
    
    [Display(Name = "Image")]
    public IFormFile? Image { get; set; }
   


}