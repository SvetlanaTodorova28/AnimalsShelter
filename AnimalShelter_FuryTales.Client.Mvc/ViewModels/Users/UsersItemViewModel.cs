using AnimalShelter_FuryTales.Client.Mvc.Models;


namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class UsersItemViewModel{
 
    public string FirstName { get; set; }
    public Guid Id { get; set; }
   
    public string UserName { get; set; }
    public string Ability { get; set; }
    public string ProfilePicture { get; set; }
   
    public IEnumerable<AnimalsItemViewModel> Animals { get; set; }
    
    public IEnumerable<DonationItemsInfoViewModel>? DonationItems { get; set; }
    
   
   
}