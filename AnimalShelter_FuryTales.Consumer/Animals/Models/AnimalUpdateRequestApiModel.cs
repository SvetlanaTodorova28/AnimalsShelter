namespace AnimalShelter_FuryTales.Consumer.Animals.Models;

public class AnimalUpdateRequestApiModel:AnimalCreateRequestApiModel{
    public Guid Id { get; set; }
    public string? ExistingImage { get; set; }
}