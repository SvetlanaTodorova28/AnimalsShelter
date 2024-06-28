using System.ComponentModel.DataAnnotations;

namespace AnimalShelter_FuryTales.Api.Dtos;

public class RegisterUserRequestDto:LoginUserRequestDto{
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
 
    public string RegistrationDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string ProfilePicture { get; set; }
   
}