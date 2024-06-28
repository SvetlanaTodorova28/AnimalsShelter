using System.ComponentModel.DataAnnotations;
using AnimalShelter_FuryTales.Client.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class UsersCreateViewModel{
   
    public string Email { get; set; }
    [Display(Name = "First Name:")]
    [Required(ErrorMessage = "Please enter your first name")]
    public string FirstName { get; set; }
    [Display(Name = "Last Name:")]
    [Required(ErrorMessage = "Please enter your first name")]
    public string LastName { get; set; }
    
    public string Ability { get; set; }
    
    
    public List<CheckBoxModel> Gender { get; set; }
    [Display(Name = "Gender")]
    public int GenderId { get; set; }
    
    public IEnumerable<SelectListItem>? Animals { get; set; }
    [Display(Name = "Animals : ")]
    public IEnumerable<Guid>? AnimalsIds { get; set; }
    
  
    
    
  
   
  

   
    
  

}