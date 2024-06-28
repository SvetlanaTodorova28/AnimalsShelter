using System.IdentityModel.Tokens.Jwt;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;


namespace AnimalShelter_FuryTales.Core.Services;

public interface IAuthenticationService{
    public Task<ResultModel<User>> RegisterUserAsync(User user, string password);
    public Task<ResultModel<string>> Login(string username, string password);
}