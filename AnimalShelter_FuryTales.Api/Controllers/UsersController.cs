

using System.Text;

using AnimalShelter_FuryTales.Api.Dtos;
using AnimalShelter_FuryTales.Api.Dtos.DonationItems;
using AnimalShelter_FuryTales.Api.Dtos.Users;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Enums;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using Scala.StockSimulation.Utilities.Authorization;

namespace AnimalShelter_FuryTales.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController:ControllerBase{
   
    private readonly UserManager<User> _userManager;
   
    private readonly RoleManager<IdentityRole> _roleManager;
    
    private readonly IAvatarService _avatarService;
    private readonly IUserService _userService;
    private readonly IAnimalService _animalService;
    private readonly IEmailService _emailService;
    

    public UsersController(IUserService userService,
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager,IAvatarService avatarService, IAnimalService animalService,
        IEmailService emailService){
    
        _userService = userService;
        _userManager = userManager;
        _roleManager = roleManager;
        _avatarService = avatarService;
        _animalService = animalService;
        _emailService = emailService;
       
        
    }
   [HttpGet("GetUsers")]
   [Authorize(Policy = GlobalConstants.AdminRoleName)]
   public async Task<ActionResult<IEnumerable<UserResponsDto>>> GetUsers()
   {
       var users = await _userManager
           .Users
           .Include(user => user.Animals).ToListAsync();
        
       var userDtos = users
           .Select(user => new UserResponsDto {
               Id = Guid.Parse(user.Id),
               UserName = user.UserName,
               FirstName = user.FirstName,
               Ability = user.Ability,
               ProfilePicture = user.ProfilePicture,
               Animals = user.Animals?.Select(animal => new AnimalResponseDto {
                   Id = Guid.Parse(animal.Id.ToString()),
                   Name = animal.Name
               }) ?? Enumerable.Empty<AnimalResponseDto>() 
           });

       return Ok(userDtos);
   }
   
   [HttpGet("GetVolunteers")]
   [AuthorizeMultiplePolicy(GlobalConstants.AdminRoleName + "," + GlobalConstants.VolunteerRoleName + "," + GlobalConstants.DonationsPolicy, false)]
   public async Task<ActionResult<IEnumerable<UserResponsDto>>> GetVolunteers(){
       var role = await _roleManager.FindByNameAsync(GlobalConstants.VolunteerRoleName);
       if (role == null){
           return NotFound("Role 'Volunteer' not found.");
       }
       var usersInRole = await _userManager.GetUsersInRoleAsync(GlobalConstants.VolunteerRoleName);
       var userDtos = usersInRole.Select(user => new UserResponsDto()
       {
           Id = Guid.Parse(user.Id),
           UserName = user.UserName,
           FirstName = user.FirstName,
           Ability = user.Ability,
           ProfilePicture = user.ProfilePicture
       });
       return Ok(userDtos);
   }
   
   [HttpGet("GetAdopters")]
   [Authorize(Policy = GlobalConstants.AdminRoleName)]
   public async Task<ActionResult<IEnumerable<UserResponsDto>>> GetAdopters(){
       var role = await _roleManager.FindByNameAsync(GlobalConstants.AdopterRoleName);
       if (role == null){
           return NotFound("Role 'Adopter' not found.");
       }
       var usersInRole = await _userManager.GetUsersInRoleAsync(GlobalConstants.AdopterRoleName);
       var userDtos = usersInRole.Select(user => new UserResponsDto()
       {
           Id = Guid.Parse(user.Id),
           UserName = user.UserName,
           FirstName = user.FirstName,
           ProfilePicture = user.ProfilePicture,
       });
         
       return Ok(userDtos);
   }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponsDto>> GetUserById(Guid id){
        
        var user = await _userManager.Users
            .Include(u => u.Animals)
            .FirstOrDefaultAsync(u => u.Id == id.ToString());


        if (user == null)
        {
            return NotFound("User not found");
        }
        var userDto = new UserResponsDto{
            Id = Guid.Parse(user.Id),
            Email = user.Email,
            UserName = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Ability = user.Ability,
            ProfilePicture = user.ProfilePicture,
            Gender = user.Gender.ToString(),
            Animals = user.Animals?.Select(animal => new AnimalResponseDto {
                Id = Guid.Parse(animal.Id.ToString()),
                Name = animal.Name
            }) ?? Enumerable.Empty<AnimalResponseDto>(),
        };
        if (user.DonationsTotal != null){
            userDto.TotalAmount = user.DonationsTotal ?? 0;
        }
        return Ok(userDto);
    }

    
    [HttpPost]
    [Authorize(Policy = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Create([FromBody] UsersCreateRequestDto model){

        var user = new User(){
            Email = model.Email,
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Ability = model.Ability,
            Gender = (Gender)Enum.Parse(typeof(Gender), model.Gender),
            
        };
        user.Animals = new List<Animal>();
        if (model.Animals != null && model.Animals.Count() > 0){
            var animalIds = model.Animals.Select(a => a.Id).ToList();
            var existingAnimals = await _animalService.GetAll()
                .Where(b => animalIds.Contains(b.Id))
                .ToListAsync(); // Zorg ervoor dat dit de oproep naar de database uitvoert en de resultaten ophaalt
            user.Animals.AddRange(existingAnimals);
        }


        user.ProfilePicture = _avatarService.GetAvatarUrl(model.Gender, model.Email);
      
        var result = await _userService.CreateUserAsync(user, model.Password);

        if (!result.Success){
            return BadRequest(result.Errors);
        }
        await SendEmailAsync(result.Data);

        return Ok(new { message = "User successfully registered", userId = result.Data.Id });
        
    }
    
    [HttpPut("UpdateUser")]
    [Authorize(Policy = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Update([FromBody]UsersUpdateRequestDto usersUpdateRequestDto)
    {
        var existingUser = await _userManager
            .Users
            .Include(u => u.Animals)
            .FirstOrDefaultAsync( u => u.Id == usersUpdateRequestDto.Id.ToString());
        if (existingUser == null)
        {
            return NotFound($"No user with id '{usersUpdateRequestDto.Id}' found.");
        }

        // Update user properties
        existingUser.UserName = usersUpdateRequestDto.Email;
        existingUser.Email = usersUpdateRequestDto.Email;
        existingUser.FirstName = usersUpdateRequestDto.FirstName;
        existingUser.LastName = usersUpdateRequestDto.LastName;
        existingUser.Ability = usersUpdateRequestDto.Ability;
        existingUser.Gender = (Gender)Enum.Parse(typeof(Gender), usersUpdateRequestDto.Gender);
        existingUser.ProfilePicture = _avatarService.GetAvatarUrl(usersUpdateRequestDto.Gender.ToString(), usersUpdateRequestDto.Email);
        

        // Manage animals if provided
        existingUser.Animals = new List<Animal>();
        if (usersUpdateRequestDto.Animals != null && usersUpdateRequestDto.Animals.Any())
        {
            existingUser.Animals.Clear();
            var animalIds = usersUpdateRequestDto.Animals.Select(a => a.Id).ToList();
            var existingAnimals = await _animalService.GetAll()
                .Where(a => animalIds.Contains(a.Id))
                .ToListAsync();
            existingUser.Animals.AddRange(existingAnimals);
        }
        
        

        var updateResult = await _userManager.UpdateAsync(existingUser);
        if (!updateResult.Succeeded)
        {
            return BadRequest(updateResult.Errors);
        }

        return Ok(new { message = "User successfully updated" });
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Delete(Guid id){
        var existingUser = await _userManager.FindByIdAsync(id.ToString());
        if (existingUser == null){
            return NotFound($"No user with id '{id}' found.");
        }

       

        var result = await _userManager.DeleteAsync(existingUser);
        if (result.Succeeded){
            return Ok(new{ message = "User successfully deleted" });
        }
        return BadRequest(result.Errors);
    }

  
    
    [HttpPost("ResetPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] UsersChangePasswordDto changePasswordDto)
    {
        var bytes = WebEncoders.Base64UrlDecode( changePasswordDto.Token);
        changePasswordDto.Token = Encoding.UTF8.GetString(bytes);
     
        
        var existingUser = await _userManager.FindByIdAsync(changePasswordDto.Id.ToString());
        if (existingUser == null){
            return NotFound("User not found.");
        }
       

        var result = await _userManager.ResetPasswordAsync(existingUser, changePasswordDto.Token, changePasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { message = "Password has been successfully reset." });
    }

    
    private async Task SendEmailAsync(User user) {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
      
        var resetPasswordUrl = $"<a href='{Environment.GetEnvironmentVariable("CLIENT_RESET_PASSWORD_URL")}/users/ResetPassword?token={token}&userId={user.Id}'>click this link to reset</a>";

        await _emailService.SendEmailAsync(user.Email, "Personalize Your Password",
            $"Please set your personal password by clicking here: {resetPasswordUrl}");
    }
    
    //update total donations
    [HttpPut("UpdateTotalDonations")]
    public async Task<IActionResult> UpdateTotalDonations([FromBody] UsersUpdateDonationsDto updateDonations)
    {
        var existingUser = await _userManager.FindByIdAsync(updateDonations.Id.ToString());
        if (existingUser == null)
        {
            return NotFound($"No user with id '{updateDonations.Id}' found.");
        }
       _userService.UpdateTotalDonations(existingUser, updateDonations.Amount);

        return Ok(new { message = "User successfully updated" });
    }
}

    