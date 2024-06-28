using AnimalShelter_FuryTales.Consumer.Animals.Models;
using AnimalShelter_FuryTales.Consumer.DonationItems;

namespace AnimalShelter_FuryTales.Consumer.Users.Models;

public class UserResponseApiModel{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public string Ability { get; set; }
    public IEnumerable<AnimalResponseApiModel>? Animals { get; set; }
    
    public string Gender { get; set; }
    
    public string ProfilePicture { get; set; }
    
    
    public decimal TotalAmount { get; set; }
    
  
   

    
   
  
    
    
}