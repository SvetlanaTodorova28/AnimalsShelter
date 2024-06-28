using AnimalShelter_FuryTales.Api.Dtos.Animals;
using AnimalShelter_FuryTales.Api.Dtos.Breeds;

namespace AnimalShelter_FuryTales.Api.Dtos.Species;

public class SpeciesResponseDto:BaseDto{
    public IEnumerable<BaseDto> Breeds { get; set; }
}