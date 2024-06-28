using System.Security.Claims;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AnimalShelter_FuryTales.Core.Services;

public class ClaimsService:IClaimService{
    
    private readonly UserManager<User> _userManager;
  
    public ClaimsService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<Claim>> GenerateClaimsForUser(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            
        };
        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);
        
        //get the newUser to add claims
        var userRoles = await _userManager.GetRolesAsync(user);
        
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        
       
        claims.Add(new Claim("FirstName", user.FirstName ?? ""));
        claims.Add(new Claim("LastName", user.LastName ?? ""));
        claims.Add(new Claim(GlobalConstants.HealthCareClaimType, user.Ability));
        claims.Add(new Claim(GlobalConstants.DonationClaimType, user.DonationsTotal.ToString()));
        
        bool isSpecialRole = userRoles.Any(role => role == GlobalConstants.AdminRoleName || role == GlobalConstants.VolunteerRoleName);
        string profileImageUrl;
        if (isSpecialRole) {
            profileImageUrl = user.ProfilePicture;
        } else {
            profileImageUrl = GlobalConstants.DefaultAvatar;
        }
        claims.Add(new Claim("ProfileImage", profileImageUrl));
       
        

        return claims;
    }
}
