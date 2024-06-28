using AnimalShelter_FuryTales.Client.Mvc.Models;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewModels;

public class DonationItemsIndexViewModel{
    public Guid Id { get; set; }
    public List<DonationItemsInfoViewModel> DonationCartItems { get; set; }
    public decimal TotalPrice { get; set; }
}