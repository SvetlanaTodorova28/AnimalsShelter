using System.ComponentModel.DataAnnotations;
using AnimalShelter_FuryTales.Api.Dtos.Animals;
using AnimalShelter_FuryTales.Api.Dtos.Species;

namespace AnimalShelter_FuryTales.Api.Dtos.Breeds;

public class BreedRequestDto:BaseDto{
    public Guid? SpeciesId { get; set; }
}