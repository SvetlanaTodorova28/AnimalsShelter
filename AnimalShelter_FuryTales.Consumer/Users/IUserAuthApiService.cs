using AnimalShelter_FuryTales.Consumer.Users.Models;

namespace AnimalShelter_FuryTales.Consumer.Users;

public interface IUserAuthApiService{
    Task<UserLoginResponseApiModel> LoginAsync(UserLoginRequestApiModel userToLogin);
    Task<UserResponseApiModel> RegisterAsync(UserRegisterRequestApiModel userToRegister);

}