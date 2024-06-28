using AnimalShelter_FuryTales.Consumer.Breeds.Models;
using AnimalShelter_FuryTales.Consumer.Species;
using AnimalShelter_FuryTales.Consumer.Users.Models;

namespace AnimalShelter_FuryTales.Consumer.Animals.Models;

public class AnimalResponseApiModel{
    public Guid Id { get; set; }
    public string Name { get; set; } 
   
    public BreedResponseApiModel? Breed { get; set; }
    
    public SpeciesResponseApiModel? Species { get; set; }
    public Guid? SpeciesId { get; set; }
    
   public IEnumerable<UserResponseApiModel>? Users { get; set; }
    
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Health { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public decimal? MonthlyFoodExpenses { get; set; }

}