using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class AnimalsUpdateViewModel:AnimalsCreateViewModel{
    [HiddenInput]
    public Guid Id { get; set; }
    
    
    public string? ExistingImage { get; set; }
    [Display(Name = "New Image")]
    public IFormFile? NewImage { get; set; }
    
}