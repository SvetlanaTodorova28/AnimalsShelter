using System.Net.Http.Headers;
using System.Net.Http.Json;
using AnimalShelter_FuryTales.Api.Dtos.Users;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Users.Models;

namespace AnimalShelter_FuryTales.Consumer.Users;

public class UserApiService:IUserApiService{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _shelterApiClient;

    public UserApiService(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
        _shelterApiClient = _httpClientFactory.CreateClient(GlobalConstants.HttpClient);
        _shelterApiClient.BaseAddress = new Uri(ApiRoutes.Users);
    }
    public async Task<UserResponseApiModel[]> GetUsersAsync(){
        var users = await _shelterApiClient.GetFromJsonAsync<UserResponseApiModel[]>("GetUsers/");
        if (users is not null) {
            return users;
        }
        return Array.Empty<UserResponseApiModel>();
    }
    public async Task<UserResponseApiModel[]> GetVolunteersAsync(string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var users = await _shelterApiClient.GetFromJsonAsync<UserResponseApiModel[]>("GetVolunteers/");
        if (users is not null) {
            return users;
        }
        return Array.Empty<UserResponseApiModel>();
    }
    public async Task<UserResponseApiModel[]> GetAdoptersAsync(string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var users = await _shelterApiClient.GetFromJsonAsync<UserResponseApiModel[]>("GetAdopters/");
        if (users is not null) {
            return users;
        }
        return Array.Empty<UserResponseApiModel>();
    }

    public async Task<UserResponseApiModel> GetUserByIdAsync(Guid id){
        var user = await _shelterApiClient.GetFromJsonAsync<UserResponseApiModel>($"{id}");
        return user;
    }
    

    public async Task CreateUsersAsync(UsersCreateRequestApiModel userToCreate, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.PostAsJsonAsync("", userToCreate);

        if (response.IsSuccessStatusCode == false)
        {
             var errorResponse = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Failed to create user: {errorResponse}");
        }
    }

    public async Task UpdateUsersAsync(UsersUpdateRequestApiModel userToUpdate, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.PutAsJsonAsync("UpdateUser/", userToUpdate);

        if (response.IsSuccessStatusCode == false)
        {
              var errorResponse = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Failed to update user: {errorResponse}");
        }
    }

    public async Task UpdateUserDonationsAsync(UsersUpdateDonationsApiModel user, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.PutAsJsonAsync("UpdateTotalDonations/", user);

        if (response.IsSuccessStatusCode == false)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Failed to update user: {errorResponse}");
        }
    }

    public async Task DeleteUsersAsync(Guid id, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _shelterApiClient.DeleteAsync($"{id}");

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            
            throw new ApplicationException($"Failed to delete user: {errorResponse}");
        }
    }
    
    public async Task ChangeUserPasswordAsync(UserChangePasswordApiModel model){
        
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);

        var response = await _shelterApiClient.PostAsJsonAsync("ResetPassword/", model);
      

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Failed to change password: {errorContent}");
        }
    }
    
    
}