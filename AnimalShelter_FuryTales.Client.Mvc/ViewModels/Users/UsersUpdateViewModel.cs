using System.ComponentModel.DataAnnotations;
using AnimalShelter_FuryTales.Client.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class UsersUpdateViewModel :UsersCreateViewModel{
    [HiddenInput]
    public Guid Id { get; set; }
    
    public string? ProfilePicture { get; set; }
    
  


    
}