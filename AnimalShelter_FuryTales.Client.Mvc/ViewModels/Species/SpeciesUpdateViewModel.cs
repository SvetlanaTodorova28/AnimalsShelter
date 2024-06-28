using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class SpeciesUpdateViewModel:SpeciesCreateViewModel{
    [HiddenInput]
    public Guid Id { get; set; }
}