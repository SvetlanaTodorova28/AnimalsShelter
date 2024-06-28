using System.Security.Claims;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Data;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter_FuryTales.Core.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IAnimalService _animalService;
    private readonly ApplicationDbContext _applicationDbContext;
    

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService,
        IAnimalService animalService, ApplicationDbContext applicationDbContext){
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _animalService = animalService;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<ResultModel<User>> CreateUserAsync(User user, string password){
        
        var newUser = new User{
            UserName = user.UserName,
            Email = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfilePicture = user.ProfilePicture,
            Ability = user.Ability,
            Gender = user.Gender,
            DonationsTotal = user.DonationsTotal
        };
        newUser.Animals = user.Animals;
        
            
        var result = await _userManager.CreateAsync(newUser, password);

        if (!result.Succeeded){
            return new ResultModel<User>{
                Errors = new List<string>(result.Errors.Select(x => x.Description))
            };
        }
        var roleResult = await _userManager.AddToRoleAsync(newUser, GlobalConstants.VolunteerRoleName);
        if (!roleResult.Succeeded) {
            
            await _userManager.DeleteAsync(newUser);
            return new ResultModel<User>{
                Errors = new List<string>(roleResult.Errors.Select(e => e.Description))
            };
        }
        
        return new ResultModel<User>{
            Data = newUser
        };
    }

    public async Task<ResultModel<User>> UpdateTotalDonations(User user, decimal donatedAmount){
        var updatedUser = await _userManager.FindByIdAsync(user.Id.ToString());
        if (updatedUser is null){
            return new ResultModel<User>{
                Errors = new List<string> { "User does not exist" }
            };
        }

        if (updatedUser.DonationsTotal is null){
            updatedUser.DonationsTotal = donatedAmount;
        }
        else{
            updatedUser.DonationsTotal += donatedAmount;
        }

        await _applicationDbContext.SaveChangesAsync();

        return new ResultModel<User>{
            Data = user
        };
    }

}
