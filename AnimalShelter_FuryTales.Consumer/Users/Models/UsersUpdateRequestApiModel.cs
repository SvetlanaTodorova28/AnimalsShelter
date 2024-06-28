
using AnimalShelter_FuryTales.Consumer.Animals.Models;

namespace AnimalShelter_FuryTales.Api.Dtos.Users;

public class UsersUpdateRequestApiModel{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
                                          
    public string Ability { get; set; }
   
                                          
    public string Gender { get; set; }
    public string ProfilePicture { get; set; }
    public IEnumerable<AnimalResponseApiModel>? Animals { get; set; }
                                          
 
    
   
    
}