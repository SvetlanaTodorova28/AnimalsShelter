namespace AnimalShelter_FuryTales.Consumer.Users.Models;

public class UserChangePasswordApiModel{
   
    public string Email { get; set; }
    public Guid Id { get; set; }

    
    public string Token { get; set; }

    
    public string NewPassword { get; set; }

    
    public string ConfirmPassword { get; set; }
    
   
  
}