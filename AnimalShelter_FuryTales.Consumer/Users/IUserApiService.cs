using AnimalShelter_FuryTales.Api.Dtos.Users;
using AnimalShelter_FuryTales.Consumer.Users.Models;

namespace AnimalShelter_FuryTales.Consumer.Users;

public interface IUserApiService{
    Task<UserResponseApiModel[]> GetUsersAsync();
    Task<UserResponseApiModel[]> GetVolunteersAsync(string token);
    Task<UserResponseApiModel[]> GetAdoptersAsync(string token);
   
    Task<UserResponseApiModel> GetUserByIdAsync(Guid id);
    Task CreateUsersAsync(UsersCreateRequestApiModel userToCreate, string token);
    Task UpdateUsersAsync(UsersUpdateRequestApiModel userToUpdate, string token);
    Task UpdateUserDonationsAsync(UsersUpdateDonationsApiModel user, string token);
    Task DeleteUsersAsync(Guid id, string token);
    Task ChangeUserPasswordAsync(UserChangePasswordApiModel model);
}