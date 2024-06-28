
using AnimalShelter_FuryTales.Api.Dtos;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Entities;

using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Mvc;


namespace AnimalShelter_FuryTales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
      
       
        private readonly IAuthenticationService _authenticationService;
        private readonly IAvatarService _avatarService;
       
        
        public AccountsController(
            IAuthenticationService authenticationService,IAvatarService avatarService)
        {
           
            _authenticationService = authenticationService;
            _avatarService = avatarService;
        }
        
       
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequestDto registerUserRequestDto){
            var userToRegister = new User
            {
                UserName = registerUserRequestDto.Username,
                FirstName = registerUserRequestDto.FirstName,
                LastName = registerUserRequestDto.LastName,
                ProfilePicture = GlobalConstants.DefaultAvatar
            };
            
            var result = await _authenticationService.RegisterUserAsync(userToRegister, registerUserRequestDto.Password);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { message = "User successfully registered", userId = result.Data.Id });
        }
        
          
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto loginUserRequestDto){
          
            var result = await _authenticationService.Login(loginUserRequestDto.Username, loginUserRequestDto.Password);
            if (!result.Success){
                return Unauthorized(new { message = "Username or password is incorrect." });
            }
            
            return Ok(new LoginUserResponseDto()
            {
                Token = result.Data
                
            });
        }
        
     


     
    }
}
