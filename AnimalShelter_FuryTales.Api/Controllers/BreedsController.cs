
using AnimalShelter_FuryTales.Api.Dtos;
using AnimalShelter_FuryTales.Api.Dtos.Breeds;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scala.StockSimulation.Utilities.Authorization;

namespace AnimalShelter_FuryTales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedsController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        private readonly IBreedService _breedService;
        private readonly ISpeciesService _speciesService;

        public BreedsController(IAnimalService animalService, IBreedService breedService,
            ISpeciesService speciesService){
            _animalService = animalService;
            _breedService = breedService;
            _speciesService = speciesService;
        }
        //========================================= GET REQUESTS ==============================================================
        [HttpGet]
        public async Task<IActionResult> Get(){
            var result = await _breedService.ListAllAsync();
            if (result.Success){
                var breedsDtos = result
                    .Data
                    .Select(x => new BreedResponseDto(){
                        Id = x.Id,
                        Name = x.Name,
                        SpeciesId = x.SpeciesId,
                    });
                return Ok(breedsDtos);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id){
            if (!await _breedService.DoesBreedIdExistAsync(id)){
                return NotFound();
            }
            var result = await _breedService.GetByIdAsync(id); ;
            if (result.Success){
                var breedDto = new BreedResponseDto(){
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                    SpeciesId = Guid.Parse(result.Data.SpeciesId.ToString())
                };
                breedDto.Species =  _speciesService
                    .GetByIdAsync(result.Data.SpeciesId ?? Guid.Empty).Result.Data.Name;
                return Ok(breedDto);
            }
            return BadRequest(result.Errors);
        }
        
      
        [HttpGet("{id}/animals")]
        public async Task<IActionResult> GetAnimals(Guid id){
            if (!await _breedService.DoesBreedIdExistAsync(id)){
                return NotFound($"Not found breed with id: {id}");
            }
            var result = await _animalService.GetByBreedIdAsync(id);
            if (result.Success){
                var animalsDto = result
                    .Data
                    .Select(a => new AnimalResponseDto{
                        Name = a.Name,
                        Id = a.Id,
                        Image = $"{Request.Scheme}://{Request.Host}/img/{a.Image}",
                    });
                return Ok(animalsDto);
            }
            return NotFound($"not found animals with breed id: {id}");
        }
        
        //========================================= POST REQUESTS ==============================================================

        [HttpPost]
        [Authorize(Policy = GlobalConstants.VolunteerRoleName)]
        public async Task<IActionResult> Add( BreedRequestDto breedRequestDto){
            var breed = new Breed{
                Name = breedRequestDto.Name,
            };
           
            if( await _breedService.DoesBreedNameExistsAsync(breed))
            {
                return BadRequest($"Breed with name: {breed.Name} already exists");
            };
           
            if(breedRequestDto.SpeciesId != null || breedRequestDto.SpeciesId.HasValue){
                var species = await
                    _speciesService
                        .GetByIdAsync(breedRequestDto.SpeciesId.Value); 
                if (species == null){
                    return BadRequest($"Species with id: {breedRequestDto.SpeciesId} not found");
                }
                breed.SpeciesId = species.Data.Id;
            }
            
            var result = await _breedService.AddAsync(breed);
          
            if(result.Success){
                var breedDto = new BreedRequestDto{
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                };
                if (result.Data.SpeciesId != null){
                    breedDto.SpeciesId = Guid.Parse(result.Data.SpeciesId.ToString());
                }
                return CreatedAtAction(nameof(Get), new {id = result.Data.Id}, breedDto);
            }
            return BadRequest(result.Errors);
        }

        [HttpPut]
        [AuthorizeMultiplePolicy(GlobalConstants.VolunteerRoleName + "," + GlobalConstants.DonationsPolicy, false)]
        public async Task<IActionResult> Update(BreedRequestDto breedRequestDto)
        {
            if (!await _breedService.DoesBreedIdExistAsync(breedRequestDto.Id))
            {
                return BadRequest($"No breed with id '{breedRequestDto.Id}' found");
            }
            if (!await _speciesService.DoesSpeciesIdExistAsync(breedRequestDto.SpeciesId.Value)){
                return BadRequest($"Species id {breedRequestDto.SpeciesId} not found");
            }

            var existingBreedResult = await _breedService.GetByIdAsync(breedRequestDto.Id);

            if (existingBreedResult.Success == false)
            {
                return BadRequest(existingBreedResult.Errors);
            }

            var existingEntity = existingBreedResult.Data;
            existingEntity.Name = breedRequestDto.Name;
            existingEntity.SpeciesId = Guid.Parse(breedRequestDto.SpeciesId.ToString());
           

            var result = await _breedService.UpdateAsync(existingEntity);

            if (result.Success)
            {
                return Ok($"Breed {existingEntity.Id} updated");
            }

            return BadRequest(result.Errors);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = GlobalConstants.VolunteerRoleName)]
        public async Task<IActionResult> Delete(Guid id){
            if (!await _breedService.DoesBreedIdExistAsync(id))
            {
                return NotFound($"Breed with id :'{id}' does noet exist !");
            }
            var existingBreed = await _breedService.GetByIdAsync(id);
            var result = await _breedService.DeleteAsync(existingBreed.Data);
            if (result.Success)
            {
                return Ok($"Breed with id : {existingBreed.Data} deleted");
            }
            return BadRequest(result.Errors);
        }
    }
}
