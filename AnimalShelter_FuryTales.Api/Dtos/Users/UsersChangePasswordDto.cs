using System.ComponentModel.DataAnnotations;

namespace AnimalShelter_FuryTales.Api.Dtos.Users;

public class UsersChangePasswordDto{
    
    public string Email { get; set; }

    public Guid Id { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "The password must be at least 4 characters long.")]
    public string NewPassword { get; set; }

    [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    
   
    
    
}