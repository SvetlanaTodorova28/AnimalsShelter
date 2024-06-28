using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnimalShelter_FuryTales.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AnimalShelter_FuryTales.Core.Services;

public class TokenService: ITokenService{
    
   
    
    private readonly IClaimService _claimService;
    private readonly IConfiguration _configuration;

    public TokenService(IClaimService claimService,
        IConfiguration configuration){
        
        _claimService = claimService;
        _configuration = configuration;
    }

    public async Task<JwtSecurityToken> GenerateTokenAsync(User user){
         var claims = await _claimService.GenerateClaimsForUser(user);
         
         var expirationDays = _configuration.GetValue<int>("JWTConfiguration:TokenExpirationDays");
         var siginingKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTConfiguration:SigningKey"));
         var token = new JwtSecurityToken
         (
             issuer: _configuration.GetValue<string>("JWTConfiguration:Issuer"),
             audience: _configuration.GetValue<string>("JWTConfiguration:Audience"),
             claims: claims,
             expires: DateTime.UtcNow.Add(TimeSpan.FromDays(expirationDays)),
             notBefore: DateTime.UtcNow,
             signingCredentials: new SigningCredentials(new SymmetricSecurityKey(siginingKey), SecurityAlgorithms.HmacSha256)
         );

         return token;
     }
     
}