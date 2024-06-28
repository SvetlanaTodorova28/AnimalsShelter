using System.Security.Claims;
using AnimalShelter_FuryTales.Core.Entities;

namespace AnimalShelter_FuryTales.Core.Services;

public interface IClaimService{
    Task<IEnumerable<Claim>> GenerateClaimsForUser(User user);
}