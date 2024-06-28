using System.Security.Claims;
using AnimalShelter_FuryTales.Constants;
using Scala.StockSimulation.Utilities.Authorization.Interfaces;

namespace Scala.StockSimulation.Utilities.Authorization;

public class AuthorizationServiceDonations : IAuthorizationServiceDonations{
    public bool UserMeetsDonationRequirement(ClaimsPrincipal user){
        return user.Claims.Any(c =>
            c.Type == GlobalConstants.DonationClaimType &&
            decimal.TryParse(c.Value, out var donationAmount) &&
            donationAmount >= GlobalConstants.DonationLevel300);
    }
}