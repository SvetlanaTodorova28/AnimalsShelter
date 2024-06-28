using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalShelter_FuryTales.Api.Dtos;
using AnimalShelter_FuryTales.Api.Dtos.Animals;
using AnimalShelter_FuryTales.Api.Dtos.Breeds;
using AnimalShelter_FuryTales.Api.Dtos.Species;
using AnimalShelter_FuryTales.Core.Enums;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        private readonly IBreedService _breedService;
        private readonly ISpeciesService _speciesService;

        public SearchController(IAnimalService animalService, IBreedService breedService,
            ISpeciesService speciesService){
            _animalService = animalService;
            _breedService = breedService;
            _speciesService = speciesService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchQuery){
            var result = await _animalService.SearchAsyncByBreedBySpecies(searchQuery);
            if (result.Success){
                var animalsDto = result
                 .Data
                 .Select(x => new AnimalResponseDto(){
                        Id = x.Id,
                        Name = x.Name,
                        Age = x.Age,
                        Gender = x.Gender.ToString(),
                        Health = x.Health.ToString(),
                        Description = x.Description,
                        Image = $"{Request.Scheme}://{Request.Host}/img/{x.Image}",
                        MonthlyFoodExpenses = x.MonthlyFoodExpenses,
                        Species = new SpeciesResponseDto(){
                            Id = x.Species.Id,
                            Name = x.Species.Name
                        },
                        Breed = new BreedResponseDto(){
                            Id = x.BreedId,
                            Name = x.Breed.Name
                        }
                    });
                return Ok(animalsDto);
            }

            return BadRequest(result.Errors);
        }
    }
}
