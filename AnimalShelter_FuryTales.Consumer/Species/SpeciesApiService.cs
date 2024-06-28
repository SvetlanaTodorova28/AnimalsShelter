using System.Net.Http.Headers;
using System.Net.Http.Json;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Breeds.Models;
using AnimalShelter_FuryTales.Consumer.Species.Models;

namespace AnimalShelter_FuryTales.Consumer.Species;

public class SpeciesApiService:ISpeciesApiService{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _shelterApiClient;
    public SpeciesApiService(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
        _shelterApiClient = _httpClientFactory.CreateClient(GlobalConstants.HttpClient);
        _shelterApiClient.BaseAddress = new Uri(ApiRoutes.Species);
    }
    
    public async Task<SpeciesResponseApiModel[]> GetSpeciesAsync(){
        var species = await 
            _shelterApiClient.GetFromJsonAsync<SpeciesResponseApiModel[]>("");
        if (species is not null) {
            return species;
        }

        return Array.Empty<SpeciesResponseApiModel>();
    }

    public async Task<SpeciesResponseApiModel> GetSpeciesByIdAsync(Guid id){
        var species = await _shelterApiClient.GetFromJsonAsync<SpeciesResponseApiModel>($"{id}");
        return species;
    }

   

    public async Task CreateSpeciesAsync(SpeciesCreateRequestApiModel speciesToCreate, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.PostAsJsonAsync("", speciesToCreate);

        if (response.IsSuccessStatusCode == false)
        {
            //welke informatie kan ik geven?
        }
    }
    

    public async Task UpdateSpeciesAsync(SpeciesUpdateRequestApiModel speciesToUpdate, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.PutAsJsonAsync("", speciesToUpdate);

        if (response.IsSuccessStatusCode == false)
        {
            //welke informatie kan ik geven?
        }
    }

    public async Task DeleteSpeciesAsync(Guid id, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.DeleteAsync($"{id}");

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            // Log de fout of gooi een uitzondering
            throw new ApplicationException($"Failed to delete species: {errorResponse}");
        }
    }
}