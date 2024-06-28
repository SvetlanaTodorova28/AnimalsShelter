namespace AnimalShelter_FuryTales.Consumer.Species.Models;

public class SpeciesUpdateRequestApiModel:SpeciesCreateRequestApiModel{
    public Guid Id { get; set; }
}