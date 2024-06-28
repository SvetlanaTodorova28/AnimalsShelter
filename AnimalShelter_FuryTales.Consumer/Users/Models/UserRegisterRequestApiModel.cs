namespace AnimalShelter_FuryTales.Consumer.Users.Models;

public class UserRegisterRequestApiModel{
    
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
 
    public string RegistrationDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
}