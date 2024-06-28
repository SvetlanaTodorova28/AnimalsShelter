using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class AccountLoginViewModel{
    [HiddenInput]
    public string? ReturnUrl { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    
}