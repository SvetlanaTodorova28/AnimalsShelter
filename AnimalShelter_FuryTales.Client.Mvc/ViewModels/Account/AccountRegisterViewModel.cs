using System.ComponentModel.DataAnnotations;
using AnimalShelter_FuryTales.Client.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class AccountRegisterViewModel: AccountLoginViewModel{
    
    
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string LastName { get; set; }

    
        [Display(Name = "Confirm Password:")]
        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match")]
        public string ConfirmPassword { get; set; }
        
      
    }

