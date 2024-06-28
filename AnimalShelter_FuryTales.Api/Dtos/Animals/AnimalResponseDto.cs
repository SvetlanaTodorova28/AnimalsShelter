
using System.ComponentModel.DataAnnotations;
using AnimalShelter_FuryTales.Api.Dtos.Users;


namespace AnimalShelter_FuryTales.Api.Dtos;

public class AnimalResponseDto:BaseDto{
    public BaseDto? Species { get; set; }
    
    public BaseDto? Breed { get; set; }
    
    public IEnumerable<UserResponsDto>? Users { get; set; }
    
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Health { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
   
    public decimal? MonthlyFoodExpenses { get; set; }
   
}