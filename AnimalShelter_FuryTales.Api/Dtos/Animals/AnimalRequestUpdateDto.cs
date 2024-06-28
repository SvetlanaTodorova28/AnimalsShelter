namespace AnimalShelter_FuryTales.Api.Dtos.Animals;

public class AnimalRequestUpdateDto:AnimalRequestWithImageDto{
    public Guid Id { get; set; }
    public string? ExistingImage { get; set; }
}