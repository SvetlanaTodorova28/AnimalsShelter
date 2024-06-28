

using AnimalShelter_FuryTales.Api.Dtos.DonationItems;

namespace AnimalShelter_FuryTales.Api.Dtos.Users;

public class UserResponsDto{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
    
    public string LastName { get; set; }
   
    public string Ability { get; set; }
    public IEnumerable<AnimalResponseDto>? Animals { get; set; }
    
    public string ProfilePicture { get; set; }
    
    
    public decimal TotalAmount { get; set; }
    
  
  
  
   
}