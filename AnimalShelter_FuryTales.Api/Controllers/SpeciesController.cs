using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalShelter_FuryTales.Api.Dtos.Animals;
using AnimalShelter_FuryTales.Api.Dtos.Breeds;
using AnimalShelter_FuryTales.Api.Dtos.Species;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace AnimalShelter_FuryTales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase{
        private readonly IAnimalService _animalService;
        private readonly IBreedService _breedService;
        private readonly ISpeciesService _speciesService;

        public SpeciesController(IAnimalService animalService, IBreedService breedService,
            ISpeciesService speciesService){
            _animalService = animalService;
            _breedService = breedService;
            _speciesService = speciesService;
        }
//========================================= GET REQUESTS ==============================================================
        [HttpGet]
        public async Task<IActionResult> Get(){
            var result = await _speciesService.ListAllAsync();
            if (result.Success){
                var speciesDtos = result
                    .Data
                    .Select(x => new SpeciesResponseDto{
                        Id = x.Id,
                        Name = x.Name,
                        Breeds = x.Breeds.Select(b => new BreedResponseDto{
                            Id = b.Id,
                            Name = b.Name
                        })
                    });
                return Ok(speciesDtos);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id){
            if (!await _speciesService.DoesSpeciesIdExistAsync(id)){
                return NotFound();
            }

            var result = await _speciesService.GetByIdAsync(id);
            if (result.Success){
                var speciesDto = new SpeciesResponseDto{
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                    Breeds = result.Data.Breeds.Select(b => new BreedResponseDto{
                        Id = b.Id,
                        Name = b.Name
                    })
                };
                return Ok(speciesDto);
            }
            return BadRequest(result.Errors);
        }
        
        //========================================= POST REQUESTS ==============================================================
       [HttpPost]
       [Authorize(Policy = GlobalConstants.VolunteerRoleName)]
        public async Task<IActionResult> Add(SpeciesRequestDto speciesRequestDto){
           //wij hebben eerst the species nodig omdat de service klasse met entity werkt
            var species = new Species{
                Name = speciesRequestDto.Name
            };
            if (await _speciesService.DoesSpeciesNameExistsAsync(species)){
                return BadRequest($"Species with name {speciesRequestDto.Name} already exists");
            }
         
            if (speciesRequestDto.Breeds != null && speciesRequestDto.Breeds.Count() > 0){
                foreach (var breedRequestDto in speciesRequestDto.Breeds){
                    if (!await _breedService.DoesBreedIdExistAsync(breedRequestDto.Id)){
                        return BadRequest($"Breed with id {breedRequestDto} does not exist");
                    }
                }
                var breedIds = speciesRequestDto.Breeds.Select(b => b.Id).ToList();
                var existingBreeds = await _breedService.GetAll()
                    .Where(b => breedIds.Contains(b.Id))
                    .ToListAsync(); 


                species.Breeds = new List<Breed>();
              species.Breeds.AddRange(existingBreeds);

            }
            var result = await _speciesService.AddAsync(species);

            if (result.Success){
                var speciesDto = new SpeciesRequestDto(){
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                };
                if (result.Data.Breeds != null && result.Data.Breeds.Count() > 0){
                    speciesDto.Breeds = result.Data.Breeds.Select(b => new BreedRequestDto{
                        Id = b.Id,
                        Name = b.Name
                    });
                }
                return CreatedAtAction(nameof(Get), new {id = result.Data.Id}, speciesDto);
            }
            return BadRequest(result.Errors);
        }
        
        [HttpPut]
        [Authorize(Policy = GlobalConstants.DonationsPolicy)]
        public async Task<IActionResult> Update(SpeciesRequestDto speciesRequestDto)
        {
            if (!await _speciesService.DoesSpeciesIdExistAsync(speciesRequestDto.Id))
            {
                return BadRequest($"No species with id '{speciesRequestDto.Id}' found");
            }

            var existingSpeciesResult = await _speciesService.GetByIdAsync(speciesRequestDto.Id);

            if (existingSpeciesResult.Success == false)
            {
                return BadRequest(existingSpeciesResult.Errors);
            }

            var existingEntity = existingSpeciesResult.Data;
            existingEntity.Name = speciesRequestDto.Name;
            
           
            
            
            if (speciesRequestDto.Breeds != null && speciesRequestDto.Breeds.Any()){
                existingEntity.Breeds.Clear();

                var breedIds = speciesRequestDto.Breeds.Select(b => b.Id).ToList();
                var existingBreeds = await _breedService.GetAll()
                    .Where(b => breedIds.Contains(b.Id))
                    .ToListAsync();
                existingEntity.Breeds = new List<Breed>();
                existingEntity.Breeds.AddRange(existingBreeds);
            }

            var result = await _speciesService.UpdateAsync(existingEntity);

            if (result.Success)
            {
                return Ok($"Species {existingEntity.Id} updated");
            }

            return BadRequest(result.Errors);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = GlobalConstants.VolunteerRoleName)]
        public async Task<IActionResult> Delete(Guid id){
            if (!await _speciesService.DoesSpeciesIdExistAsync(id))
            {
                return NotFound($"Species with id :'{id}' does not exist !");
            }
            var existingSpecies = await _speciesService.GetByIdAsync(id);
            var result = await _speciesService.DeleteAsync(existingSpecies.Data);
            if (result.Success)
            {
                return Ok($"Species with id : {existingSpecies.Data.Id} deleted");
            }
            return BadRequest(result.Errors);
        }
    }
}
