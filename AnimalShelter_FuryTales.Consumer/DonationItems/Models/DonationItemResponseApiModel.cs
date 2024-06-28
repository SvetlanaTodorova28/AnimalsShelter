

namespace AnimalShelter_FuryTales.Consumer.DonationItems;

public class DonationItemResponseApiModel{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid AnimalId { get; set; }
    public decimal Amount { get; set; }

  
}