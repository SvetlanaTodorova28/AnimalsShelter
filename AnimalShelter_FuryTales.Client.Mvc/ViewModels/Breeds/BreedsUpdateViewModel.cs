using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class BreedsUpdateViewModel:BreedsCreateViewModel{
    [HiddenInput]
    public Guid Id { get; set; }
}