using System.Security.Claims;

namespace Scala.StockSimulation.Utilities.Authorization.Interfaces;

public interface IAuthorizationServiceDonations{
    public bool UserMeetsDonationRequirement(ClaimsPrincipal user);
}