using AnimalShelter_FuryTales.Client.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.Models;

public class DonationItemsInfoViewModel{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [HiddenInput]
    public Guid AnimalId { get; set; }
    public decimal Amount { get; set; }
    
    public int Quantity { get; set; }

  
    public  string AnimalName { get; set; }
}