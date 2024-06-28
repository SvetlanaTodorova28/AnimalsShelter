namespace AnimalShelter_FuryTales.Consumer.Breeds.Models;

public class BreedResponseApiModel{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public Guid? SpeciesId { get; set; }
    public string Species { get; set; }
 
}