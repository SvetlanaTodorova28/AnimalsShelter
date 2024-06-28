using System.Security.Claims;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;

namespace AnimalShelter_FuryTales.Core.Services;

public interface IUserService{

    Task<ResultModel<User>> CreateUserAsync(User user, string password);
    
    Task<ResultModel<User>> UpdateTotalDonations(User user, decimal donatedAmount);



}