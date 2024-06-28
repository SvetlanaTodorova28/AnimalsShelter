using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class UsersResetPasswordViewModel{
    public string Token { get; set; }
    public string Email { get; set; }
    [HiddenInput]
    public Guid Id { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
   

    [Required(ErrorMessage = "Please confirm your password")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    
  
}