namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class SpeciesItemViewModel:BaseViewModel{
    public IEnumerable<BreedsItemViewModel>? Breeds { get; set; }
}