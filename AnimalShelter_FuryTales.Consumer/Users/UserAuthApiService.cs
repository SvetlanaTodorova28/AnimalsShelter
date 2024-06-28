using System.Net.Http.Json;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Users.Models;

namespace AnimalShelter_FuryTales.Consumer.Users;

public class UserAuthApiService:IUserAuthApiService{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _shelterApiClient;

    public UserAuthApiService(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
        _shelterApiClient = _httpClientFactory.CreateClient(GlobalConstants.HttpClient);
        _shelterApiClient.BaseAddress = new Uri(ApiRoutes.Auth);
    }


    public async Task<UserLoginResponseApiModel> LoginAsync(UserLoginRequestApiModel userToLogin){
        
        var response = await _shelterApiClient.PostAsJsonAsync("login/", userToLogin);
        if (response.IsSuccessStatusCode){
            var user = await response.Content.ReadFromJsonAsync<UserLoginResponseApiModel>();
            return user;
        }
        return null;
        
        
    }
    public async Task<UserResponseApiModel> RegisterAsync(UserRegisterRequestApiModel userTorRegister) {
      
        var response = await _shelterApiClient.PostAsJsonAsync("register/", userTorRegister);
        if (response.IsSuccessStatusCode) {
            var user = await response.Content.ReadFromJsonAsync<UserResponseApiModel>();
            return user;
        }
        return null;
    }

   
   
}
