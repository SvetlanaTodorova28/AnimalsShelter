using System.ComponentModel.DataAnnotations;

namespace AnimalShelter_FuryTales.Api.Dtos;

public class LoginUserRequestDto{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    
}