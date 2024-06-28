using AnimalShelter_FuryTales.Api.Dtos.Animals;
using AnimalShelter_FuryTales.Api.Dtos.Breeds;

namespace AnimalShelter_FuryTales.Api.Dtos.Species;

public class SpeciesRequestDto:BaseDto{
    
    public IEnumerable<BreedRequestDto>? Breeds { get; set; }
}