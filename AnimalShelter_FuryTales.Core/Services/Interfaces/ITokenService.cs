using System.IdentityModel.Tokens.Jwt;
using AnimalShelter_FuryTales.Core.Entities;

namespace AnimalShelter_FuryTales.Core.Services;

public interface ITokenService{
    Task<JwtSecurityToken> GenerateTokenAsync(User user);
}