using AnimalShelter_FuryTales.Api.Dtos.Users;

namespace AnimalShelter_FuryTales.Api.Dtos.DonationItems;

public class DonationItemResponseDto{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid AnimalId { get; set; }
    public decimal Amount { get; set; }

   
}