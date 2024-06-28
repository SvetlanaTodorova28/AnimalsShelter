
using AnimalShelter_FuryTales.Api.Dtos;
using AnimalShelter_FuryTales.Api.Dtos.Animals;
using AnimalShelter_FuryTales.Api.Dtos.Breeds;
using AnimalShelter_FuryTales.Api.Dtos.Species;
using AnimalShelter_FuryTales.Api.Dtos.Users;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Enums;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace AnimalShelter_FuryTales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase{
        private readonly IAnimalService _animalService;
        private readonly IBreedService _breedService;
        private readonly ISpeciesService _speciesService;
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;

        public AnimalsController(IAnimalService animalService, IBreedService breedService,
            ISpeciesService speciesService, IImageService imageService, UserManager<User> userManager){
            _animalService = animalService;
            _breedService = breedService;
            _speciesService = speciesService;
            _imageService = imageService;
            _userManager = userManager;
        }

        //========================================= GET REQUESTS ==============================================================
        [HttpGet]
       
        public async Task<IActionResult> Get(){
            var result = await _animalService.ListAllAsync();
            if (result.Success){
                var animalsDto = result
                    .Data
                    .Select(x => new AnimalResponseDto(){
                        Id = x.Id,
                        Name = x.Name,
                        Age = x.Age,
                        Gender = x.Gender.ToText(),
                        Health = x.Health.ToText(),
                        Description = x.Description,
                        Image = $"{Request.Scheme}://{Request.Host}/img/{x.Image}",
                        MonthlyFoodExpenses = x.MonthlyFoodExpenses,
                        Species = new SpeciesResponseDto(){
                            Id = x.Species.Id,
                            Name = x.Species.Name
                        },
                        Breed = new BreedResponseDto(){
                            Id = x.Breed.Id,
                            Name = x.Breed.Name
                        },
                        Users = x.Users.Select(y => new UserResponsDto(){
                            Id = Guid.Parse(y.Id),
                            FirstName = y.UserName
                        })
                    });
                return Ok(animalsDto);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id){
            if (!await _animalService.DoesAnimalIdExistsAsync(id)){
                return NotFound();
            }

            var result = await _animalService.GetByIdAsync(id);
            if (result.Success){
                var animalDto = new AnimalResponseDto(){
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                    Species = new SpeciesResponseDto(){
                        Id = result.Data.Species.Id,
                        Name = result.Data.Species.Name
                    },
                    Breed = new BreedResponseDto(){
                        Id = result.Data.Breed.Id,
                        Name = result.Data.Breed.Name
                    },
                    Age = result.Data.Age,
                    Gender = result.Data.Gender.ToText(),
                    Health = result.Data.Health.ToText(),
                    MonthlyFoodExpenses = result.Data.MonthlyFoodExpenses,
                    Description = result.Data.Description,
                    Image = $"{Request.Scheme}://{Request.Host}/img/{result.Data.Image}",
                    Users = result.Data.Users.Select(u => new UserResponsDto(){
                        Id = Guid.Parse(u.Id),
                        FirstName = u.FirstName
                    })
                };
                if (String.IsNullOrEmpty(animalDto.Image)){
                    animalDto.Image = $"{Request.Scheme}://{Request.Host}/img/default.jpg";
                }
                return Ok(animalDto);
            }

            return BadRequest(result.Errors);
        }

        //========================================= POST REQUESTS ==============================================================

        [HttpPost]
        [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
        public async Task<IActionResult> Add(AnimalRequestDto animalRequestDto){
            if (!await _breedService.DoesBreedIdExistAsync(animalRequestDto.BreedId)){
                return BadRequest($"Breed id {animalRequestDto.BreedId} not found");
            }

            if (!await _speciesService.DoesSpeciesIdExistAsync(animalRequestDto.SpeciesId)){
                return BadRequest($"Species id {animalRequestDto.SpeciesId} not found");
            }
            

            var animal = new Animal(){
                    Name = animalRequestDto.Name,
                    Age = animalRequestDto.Age,
                    Gender = (Gender)Enum.Parse(typeof(Gender), animalRequestDto.Gender),
                    Description = animalRequestDto.Description,
                    BreedId = animalRequestDto.BreedId,
                    SpeciesId = animalRequestDto.SpeciesId,
                    MonthlyFoodExpenses = animalRequestDto.Donation,
                    Image = "default.jpg"
                  
                };
            string input = animalRequestDto.Health;
            string normalizedInput = input.Replace(" ", "_");
            animal.Health = (Health)Enum.Parse(typeof(Health), normalizedInput);
            
            animal.Users = new List<User>();
            
            if (animalRequestDto.UsersIds != null && animalRequestDto.UsersIds.Any())
            {
                foreach (var userId in animalRequestDto.UsersIds){
                    if ( _userManager.FindByIdAsync(userId).Result == null){
                        return BadRequest($"Adopter with id {userId} does not exist");
                    }
                }
                var usersIds = animalRequestDto.UsersIds.Select(id => id.ToString()).ToList();
                var existingUsers = await _userManager.Users
                    .Where(b => usersIds.Contains(b.Id))
                    .ToListAsync();

                animal.Users.AddRange(existingUsers);
            }


            var result = await _animalService.AddAsync(animal);
                if (result.Success){
                    var resultSpecies = await _speciesService.GetByIdAsync(animal.SpeciesId);
                    var resultBreed = await _breedService.GetByIdAsync(animal.BreedId);
                    if (resultBreed.Success && resultSpecies.Success){
                        var animalDto = new AnimalResponseDto(){
                            Id = animal.Id,
                            Name = animal.Name,
                            Species = new SpeciesResponseDto(){
                                Id = resultSpecies.Data.Id,
                                Name = resultSpecies.Data.Name
                            },
                            Breed = new BreedResponseDto(){
                                Id = resultBreed.Data.Id,
                                Name = resultBreed.Data.Name
                            },
                            Age = result.Data.Age,
                            Gender = result.Data.Gender.ToText(),
                            Health = result.Data.Health.ToText(),
                            Description = result.Data.Description?? "",
                            Image = $"{Request.Scheme}://{Request.Host}/img/{result.Data.Image}",
                            MonthlyFoodExpenses = result.Data.MonthlyFoodExpenses,
                            Users = result.Data
                                .Users
                                .Select(x => new UserResponsDto(){
                                    Id = Guid.Parse(x.Id),
                                    FirstName = x.UserName,
                                })
                        };
                        return CreatedAtAction(nameof(Get), new{ id = result.Data.Id }, animalDto);
                    }

                    return BadRequest(result.Errors);
                }

                return BadRequest(result.Errors);
            }
      

        [HttpPost("add-with-image")]
        [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
        public async Task<IActionResult> AddWithImage([FromForm] AnimalRequestWithImageDto animalRequestWithImage){
               if (!await _breedService.DoesBreedIdExistAsync(animalRequestWithImage.BreedId)){
                return BadRequest($"Breed id {animalRequestWithImage.BreedId} not found");
            }

            if (!await _speciesService.DoesSpeciesIdExistAsync(animalRequestWithImage.SpeciesId)){
                return BadRequest($"Species id {animalRequestWithImage.SpeciesId} not found");
            }
            

            var animal = new Animal(){
                    Name = animalRequestWithImage.Name,
                    Age = animalRequestWithImage.Age,
                    Gender = (Gender)Enum.Parse(typeof(Gender), animalRequestWithImage.Gender),
                    Description = animalRequestWithImage.Description,
                    BreedId = animalRequestWithImage.BreedId,
                    SpeciesId = animalRequestWithImage.SpeciesId,
                    MonthlyFoodExpenses = animalRequestWithImage.Donation,
                
                };
            string input = animalRequestWithImage.Health;
            string normalizedInput = input.Replace(" ", "_");
            
            animal.Health = (Health)Enum.Parse(typeof(Health), normalizedInput);
            animal.Users = new List<User>();
            
            if (animalRequestWithImage.UsersIds != null && animalRequestWithImage.UsersIds.Any())
            {
                foreach (var userId in animalRequestWithImage.UsersIds){
                    if (await _userManager.FindByIdAsync(userId) == null){
                        return BadRequest($"Adopter with id {userId} does not exist");
                    }
                }
                var usersIds = animalRequestWithImage.UsersIds.Select(id => id.ToString()).ToList();
                var existingUsers = await _userManager.Users
                    .Where(b => usersIds.Contains(b.Id))
                    .ToListAsync();
                animal.Users.AddRange(existingUsers);
            }

            if (animalRequestWithImage.Image != null){
                var resultImage = await _imageService.AddOrUpdateImageAsync(animalRequestWithImage.Image);
                if (resultImage.Success){
                    animal.Image = resultImage.Data;
                }
            }

           
            var resultAnimal = await _animalService.AddAsync(animal);
                if (resultAnimal.Success){
                    var resultSpecies = await _speciesService.GetByIdAsync(animal.SpeciesId);
                    var resultBreed = await _breedService.GetByIdAsync(animal.BreedId);
                    if (resultBreed.Success && resultSpecies.Success){
                        var animalDto = new AnimalResponseDto(){
                            Id = animal.Id,
                            Name = animal.Name,
                            Species = new SpeciesResponseDto(){
                                Id = resultSpecies.Data.Id,
                                Name = resultSpecies.Data.Name
                            },
                            Breed = new BreedResponseDto(){
                                Id = resultBreed.Data.Id,
                                Name = resultBreed.Data.Name
                            },
                            Age = resultAnimal.Data.Age,
                            Gender = resultAnimal.Data.Gender.ToText(),
                            Health = resultAnimal.Data.Health.ToText(),
                            Description = resultAnimal.Data.Description,
                            Image = $"{Request.Scheme}://{Request.Host}/img/{animal.Image}",
                            MonthlyFoodExpenses = resultAnimal.Data.MonthlyFoodExpenses,
                            Users = resultAnimal.Data
                                .Users
                                .Select(x => new UserResponsDto(){
                                    Id = Guid.Parse(x.Id),
                                    FirstName = x.UserName,
                                })
                        };
                        
                        return CreatedAtAction(nameof(Get), new{ id = resultAnimal.Data.Id }, animalDto);
                    }

                    return BadRequest(resultAnimal.Errors);
                }

                return BadRequest(resultAnimal.Errors);
        }

        [HttpPut]
        [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
        public async Task<IActionResult> Update(AnimalRequestUpdateDto animalRequestUpdateDto){
            if (!await _animalService.DoesAnimalIdExistsAsync(animalRequestUpdateDto.Id)){
                return BadRequest($"No animal with id '{animalRequestUpdateDto.Id}' found");
            }

            if (!await _breedService.DoesBreedIdExistAsync(animalRequestUpdateDto.BreedId)){
                return BadRequest($"No breed with id '{animalRequestUpdateDto.BreedId}' found");
            }

            if (!await _speciesService.DoesSpeciesIdExistAsync(animalRequestUpdateDto.SpeciesId)){
                return BadRequest($"Species id {animalRequestUpdateDto.SpeciesId} not found");
            }

            var existingAnimalResult = await _animalService.GetByIdAsync(animalRequestUpdateDto.Id);

            if (existingAnimalResult.Success == false){
                return BadRequest(existingAnimalResult.Errors);
            }

            var existingEntity = existingAnimalResult.Data;
            existingEntity.Id = animalRequestUpdateDto.Id;
            existingEntity.Name = animalRequestUpdateDto.Name;
            existingEntity.BreedId = animalRequestUpdateDto.BreedId;
            existingEntity.SpeciesId = animalRequestUpdateDto.SpeciesId;
            existingEntity.Age = animalRequestUpdateDto.Age;
            existingEntity.Gender = (Gender)Enum.Parse(typeof(Gender), animalRequestUpdateDto.Gender);
            existingEntity.Description = animalRequestUpdateDto.Description;
            existingEntity.MonthlyFoodExpenses = animalRequestUpdateDto.Donation;
          
            string input = animalRequestUpdateDto.Health;
            string normalizedInput = input.Replace(" ", "_");
            existingEntity.Health = (Health)Enum.Parse(typeof(Health), normalizedInput);
            
            if (animalRequestUpdateDto.UsersIds != null && animalRequestUpdateDto.UsersIds.Any()){
                existingEntity.Users.Clear();
                foreach (var userId in animalRequestUpdateDto.UsersIds){
                    if (await _userManager.FindByIdAsync(userId) == null){
                        return BadRequest($"Adopter with id {userId} does not exist");
                    }
                }
            }

            if (animalRequestUpdateDto.UsersIds != null && animalRequestUpdateDto.UsersIds.Any()){
                var usersIds = animalRequestUpdateDto.UsersIds;
                var existingUsers = await _userManager.Users
                    .Where(b => usersIds.Contains(b.Id))
                    .ToListAsync();
                if (!existingEntity.Users.Any()){
                    existingEntity.Users = new List<User>();
                }

                existingEntity.Users.AddRange(existingUsers);
            }
          
            var resultAnimal = await _animalService.UpdateAsync(existingEntity);
            if (resultAnimal.Success){
                return Ok($"Animal {existingEntity.Id} updated");
            }
            return BadRequest(resultAnimal.Errors);
            }

        [HttpPut("update-with-image")]
        [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
        public async Task<IActionResult> UpdateWithImage([FromForm] AnimalRequestUpdateDto animalRequestUpdateDto){
            if (!await _animalService.DoesAnimalIdExistsAsync(animalRequestUpdateDto.Id)){
                return BadRequest($"No animal with id '{animalRequestUpdateDto.Id}' found");
            }

            if (!await _breedService.DoesBreedIdExistAsync(animalRequestUpdateDto.BreedId)){
                return BadRequest($"No breed with id '{animalRequestUpdateDto.BreedId}' found");
            }

            if (!await _speciesService.DoesSpeciesIdExistAsync(animalRequestUpdateDto.SpeciesId)){
                return BadRequest($"Species id {animalRequestUpdateDto.SpeciesId} not found");
            }

            var existingAnimalResult = await _animalService.GetByIdAsync(animalRequestUpdateDto.Id);

            if (existingAnimalResult.Success == false){
                return BadRequest(existingAnimalResult.Errors);
            }

            var existingEntity = existingAnimalResult.Data;
            existingEntity.Id = animalRequestUpdateDto.Id;
            existingEntity.Name = animalRequestUpdateDto.Name;
            existingEntity.BreedId = animalRequestUpdateDto.BreedId;
            existingEntity.SpeciesId = animalRequestUpdateDto.SpeciesId;
            existingEntity.Age = animalRequestUpdateDto.Age;
            existingEntity.Gender = (Gender)Enum.Parse(typeof(Gender), animalRequestUpdateDto.Gender);
            existingEntity.Description = animalRequestUpdateDto.Description;
            existingEntity.MonthlyFoodExpenses = animalRequestUpdateDto.Donation;
            
            if (animalRequestUpdateDto.Image != null){
                var resultImage = await _imageService.AddOrUpdateImageAsync(animalRequestUpdateDto.Image);
                if (resultImage.Success){
                    existingEntity.Image = resultImage.Data;
                }
            }
            
            string input = animalRequestUpdateDto.Health;
            string normalizedInput = input.Replace(" ", "_");
            existingEntity.Health = (Health)Enum.Parse(typeof(Health), normalizedInput);
            
            if (animalRequestUpdateDto.UsersIds != null && animalRequestUpdateDto.UsersIds.Any()){
                existingEntity.Users.Clear();
                foreach (var userId in animalRequestUpdateDto.UsersIds){
                    if (await _userManager.FindByIdAsync(userId) == null){
                        return BadRequest($"Adopter with id {userId} does not exist");
                    }
                }
            }

            if (animalRequestUpdateDto.UsersIds != null && animalRequestUpdateDto.UsersIds.Any()){
                var usersIds = animalRequestUpdateDto.UsersIds;
                var existingUsers = await _userManager.Users
                    .Where(b => usersIds.Contains(b.Id))
                    .ToListAsync();
                if (!existingEntity.Users.Any()){
                    existingEntity.Users = new List<User>();
                }

                existingEntity.Users.AddRange(existingUsers);
            }
          
            var resultAnimal = await _animalService.UpdateAsync(existingEntity);
            if (resultAnimal.Success){
                return Ok($"Animal {existingEntity.Id} updated");
            }
            return BadRequest(resultAnimal.Errors);
            }
    

        [HttpDelete("{id}")]
        [Authorize(Policy = GlobalConstants.HealthCarePolicy)]
        public async Task<IActionResult> Delete(Guid id){
            if (!await _animalService.DoesAnimalIdExistsAsync(id))
            {
                return NotFound($"Animal with id :'{id}' does noet exist !");
            }
            var existingAnimal = await _animalService.GetByIdAsync(id);
            
            if (!String.IsNullOrEmpty(existingAnimal.Data.Image)){
                _imageService.Delete(existingAnimal.Data.Image);
            }
            var result = await _animalService.DeleteAsync(existingAnimal.Data);
            if (result.Success)
            {
                return Ok($"Animal with id : {existingAnimal.Data.Id} deleted");
            }
            return BadRequest(result.Errors);
        }

    }

}
