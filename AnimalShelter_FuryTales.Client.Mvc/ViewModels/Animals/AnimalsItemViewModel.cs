using AnimalShelter_FuryTales.Client.Mvc.ViewModels;
using AnimalShelter_FuryTales.Consumer.Breeds.Models;
using AnimalShelter_FuryTales.Consumer.Species;


namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class AnimalsItemViewModel:BaseViewModel{
    public SpeciesResponseApiModel? Species { get; set; }
   
    public BreedResponseApiModel? Breed { get; set; }
    
    public IEnumerable<BaseViewModel>? Users { get; set; }
    
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Health { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public decimal? Donation { get; set; }
}