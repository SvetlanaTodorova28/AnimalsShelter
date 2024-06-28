using System.ComponentModel.DataAnnotations;


namespace AnimalShelter_FuryTales.Api.Dtos.Animals;

public class AnimalRequestDto:BaseDto{
    
  
    public Guid SpeciesId { get; set; }
    
    public Guid BreedId { get; set; }
    public IEnumerable<string>? UsersIds { get; set; }
   
    public int? Age { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Gender { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Health { get; set; }
    public string? Description { get; set; }
  
   
    public decimal? Donation { get; set; }
    
    
}