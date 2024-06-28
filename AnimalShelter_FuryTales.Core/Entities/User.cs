using AnimalShelter_FuryTales.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace AnimalShelter_FuryTales.Core.Entities;

public class User:IdentityUser{
    public virtual ICollection<Animal>? Animals { get; set; }
   
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string? Ability { get; set; }
    public string Email { get; set; }
    
    public string ProfilePicture { get; set; }
    public Gender Gender { get; set; }
    
   
    public decimal? DonationsTotal { get; set; }
    
}