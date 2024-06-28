using AnimalShelter_FuryTales.Consumer.Breeds.Models;

namespace AnimalShelter_FuryTales.Consumer.Species;

public class SpeciesResponseApiModel{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<BreedResponseApiModel> Breeds { get; set; }
}