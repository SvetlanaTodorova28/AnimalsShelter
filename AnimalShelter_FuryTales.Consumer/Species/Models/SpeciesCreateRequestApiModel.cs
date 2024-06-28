using AnimalShelter_FuryTales.Consumer.Breeds.Models;

namespace AnimalShelter_FuryTales.Consumer.Species.Models;

public class SpeciesCreateRequestApiModel{
    public string Name { get; set; }
    public IEnumerable<BreedResponseApiModel> Breeds { get; set; }
}