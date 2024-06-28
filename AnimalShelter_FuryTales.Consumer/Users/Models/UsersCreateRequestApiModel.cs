using AnimalShelter_FuryTales.Consumer.Animals.Models;

namespace AnimalShelter_FuryTales.Consumer.Users.Models;

public class UsersCreateRequestApiModel{
  
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    //default first
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Ability { get; set; }
    public List<AnimalResponseApiModel>? Animals{ get; set; }
    public string ProfilePicture { get; set; }
    public string Gender { get; set; }
    
 
}