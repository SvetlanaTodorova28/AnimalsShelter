using AnimalShelter_FuryTales.Api.Dtos.Animals;
using AnimalShelter_FuryTales.Api.Dtos.Species;

namespace AnimalShelter_FuryTales.Api.Dtos.Breeds;

public class BreedResponseDto:BaseDto{
    
    public Guid? SpeciesId { get; set; }
    public string Species { get; set; }
   
}