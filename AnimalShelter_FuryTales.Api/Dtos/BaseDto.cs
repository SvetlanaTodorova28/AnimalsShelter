using System.ComponentModel.DataAnnotations;

namespace AnimalShelter_FuryTales.Api.Dtos;

public abstract class BaseDto{
    
    public Guid Id { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; set; }
}