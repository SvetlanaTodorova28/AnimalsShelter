namespace AnimalShelter_FuryTales.Consumer.DonationItems;

public interface IDonationApiService{
    Task<DonationItemResponseApiModel[]> GetDonationsAsync();
    Task<DonationItemResponseApiModel> GetDonationByIdAsync(Guid id);
    Task CreateDonationItemAsync(DonationItemResponseApiModel donationToCreate, string token);
    Task DeleteDonationAsync(Guid id, string token);
}