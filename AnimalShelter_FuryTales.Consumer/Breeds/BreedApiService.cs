using System.Net.Http.Headers;
using System.Net.Http.Json;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Animals.Models;
using AnimalShelter_FuryTales.Consumer.Breeds.Models;

namespace AnimalShelter_FuryTales.Consumer.Breeds;

public class BreedApiService: IBreedApiService{
   
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _shelterApiClient;
    public BreedApiService(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
        _shelterApiClient = _httpClientFactory.CreateClient(GlobalConstants.HttpClient);
        _shelterApiClient.BaseAddress = new Uri(ApiRoutes.Breeds);
    }
    
    public async Task<BreedResponseApiModel[]> GetBreedsAsync(){
        
        var breeds = await 
            _shelterApiClient.GetFromJsonAsync<BreedResponseApiModel[]>("");
        if (breeds is not null) {
            return breeds;
        }
        return Array.Empty<BreedResponseApiModel>();
    }

    public async Task<BreedResponseApiModel> GetBreedByIdAsync(Guid id){
        var breed = await _shelterApiClient.GetFromJsonAsync<BreedResponseApiModel>($"{id}");
        return breed;
    }

    public async Task<AnimalResponseApiModel[]> GetAnimalsByBreedIdAsync(Guid id){
        var response = await _shelterApiClient.GetAsync($"{id}/animals");
        
        if (response.IsSuccessStatusCode){
            var animalsFromBreed = await response.Content.ReadFromJsonAsync<AnimalResponseApiModel[]>();
            return animalsFromBreed;
        }
        return Array.Empty<AnimalResponseApiModel>();
    }


    public async Task CreateBreedAsync(BreedCreateRequestApiModel breedToCreate, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.PostAsJsonAsync("", breedToCreate);

        if (response.IsSuccessStatusCode == false)
        {
            //welke informatie kan ik geven?
        }
    }

    public async Task UpdateBreedAsync(BreedUpdateRequestApiModel breedToUpdate, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.PutAsJsonAsync("", breedToUpdate);

        if (response.IsSuccessStatusCode == false)
        {
            //welke informatie kan ik geven?
        }
    }

    public async Task DeleteBreedAsync(Guid id, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.DeleteAsync($"{id}");

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            // Log de fout of gooi een uitzondering
            throw new ApplicationException($"Failed to delete breed: {errorResponse}");
        }
    }
}