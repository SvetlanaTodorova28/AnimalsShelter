using System.Net.Http.Headers;
using System.Net.Http.Json;
using AnimalShelter_FuryTales.Constants;

namespace AnimalShelter_FuryTales.Consumer.DonationItems;

public class DonationApiService: IDonationApiService{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _shelterApiClient;
    public DonationApiService(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
        _shelterApiClient = _httpClientFactory.CreateClient(GlobalConstants.HttpClient);
        _shelterApiClient.BaseAddress = new Uri(ApiRoutes.Donations);
    }
    
    public async Task<DonationItemResponseApiModel[]> GetDonationsAsync(){
        
        var donations = await 
            _shelterApiClient.GetFromJsonAsync<DonationItemResponseApiModel[]>("");
        if (donations is not null) {
            return donations;
        }
        return Array.Empty<DonationItemResponseApiModel>();
    }

    public async Task<DonationItemResponseApiModel> GetDonationByIdAsync(Guid id){
        var donationItem = await _shelterApiClient.GetFromJsonAsync<DonationItemResponseApiModel>($"{id}");
        return donationItem;
    }

    public async Task CreateDonationItemAsync(DonationItemResponseApiModel donationItemToCreate, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.PostAsJsonAsync("", donationItemToCreate);

        if (response.IsSuccessStatusCode == false)
        {
            
        }
    }
    
    
    public async Task DeleteDonationAsync(Guid id, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.DeleteAsync($"{id}");

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            // Log de fout of gooi een uitzondering
            throw new ApplicationException($"Failed to delete donation: {errorResponse}");
        }
    }
}