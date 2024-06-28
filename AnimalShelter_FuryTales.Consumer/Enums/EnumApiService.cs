using System.Net.Http.Json;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Breeds.Models;

namespace AnimalShelter_FuryTales.Consumer.Enums;

public class EnumApiService:IEnumApiService{
    
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _shelterApiClient;
    public EnumApiService(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
        _shelterApiClient = _httpClientFactory.CreateClient(GlobalConstants.HttpClient);
        _shelterApiClient.BaseAddress = new Uri(ApiRoutes.Enums);
    }
    public async Task<string[]> GetHealthAsync(){
        var health = await 
            _shelterApiClient.GetFromJsonAsync<string[]>("Health/");
        if (health is not null) {
            return health;
        }
        return Array.Empty<string>();
    }

    public async Task<string[]> GetGenderAsync(){
        var gender = await 
            _shelterApiClient.GetFromJsonAsync<string[]>("Gender/");
        if (gender is not null) {
            return gender;
        }
        return Array.Empty<string>();
    }
}