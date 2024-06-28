using System.ComponentModel.DataAnnotations;

namespace AnimalShelter_FuryTales.Api.Dtos.Animals;

public class AnimalRequestWithImageDto:AnimalRequestDto{
    
    public IFormFile Image { get; set; }

    [FileExtensions(Extensions="jpg,jpeg,png")]
    public string FileName => Image?.FileName;
}