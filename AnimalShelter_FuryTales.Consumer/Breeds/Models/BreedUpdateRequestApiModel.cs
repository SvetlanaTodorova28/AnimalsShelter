namespace AnimalShelter_FuryTales.Consumer.Breeds.Models;

public class BreedUpdateRequestApiModel{
 
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? SpeciesId { get; set; }
}