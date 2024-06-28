namespace AnimalShelter_FuryTales.Consumer.Breeds.Models;

public class BreedCreateRequestApiModel{
    public string Name { get; set; }
    public Guid SpeciesId { get; set; }
}