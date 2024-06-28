

using AnimalShelter_FuryTales.Client.Mvc.ViewModels;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class BreedsIndexViewModel{
    public IEnumerable<BreedsItemViewModel> Breeds{ get; set; }
}