using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AnimalShelter_FuryTales.Consumer.Animals.Models;

public class AnimalCreateRequestApiModel{
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    public Guid SpeciesId { get; set; }
   
   
    public Guid BreedId { get; set; }
    public List<string>? UsersIds { get; set; }
   
    public int? Age { get; set; }
    [Required(ErrorMessage = "Gender is required")]
    public string Gender { get; set; }
    [Required(ErrorMessage = "Health is required")]
    public string Health { get; set; }
    public string? Description { get; set; }
  
    public decimal? Donation { get; set; }
    public IFormFile? Image { get; set; }

}