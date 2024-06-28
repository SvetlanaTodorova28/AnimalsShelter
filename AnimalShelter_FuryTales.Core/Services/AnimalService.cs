using AnimalShelter_FuryTales.Core.Data;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter_FuryTales.Core;

public class AnimalService:IAnimalService{
    
    private readonly ApplicationDbContext _applicationDbContext;

    public AnimalService(ApplicationDbContext applicationDbContext){
        _applicationDbContext = applicationDbContext;
    }

    public IQueryable<Animal> GetAll(){
       return _applicationDbContext
           .Animals
           .Include(x=>x.Species).
           Include(x=>x.Breed);
    }

    public async Task<ResultModel<IEnumerable<Animal>>> ListAllAsync(){
        var animals = await _applicationDbContext
            .Animals
            .Include(a => a.Species)
            .Include(a => a.Breed)
            .Include(a => a.Users)
            .ToListAsync();
        var resultModel = new ResultModel<IEnumerable<Animal>>()
        {
            Data = animals,
        };
        return resultModel;
    }

    public async Task<ResultModel<Animal>> GetByIdAsync(Guid id){
        var animal = await _applicationDbContext
            .Animals
            .Include(a => a.Species)
            .Include(a => a.Breed)
            .Include(a => a.Users)
            .FirstOrDefaultAsync(p => p.Id == id);

        if(animal is null)
        {
            return new ResultModel<Animal>
            {
                Errors = new List<string> { "Animal does not exist" }
            };

        }
        return new ResultModel<Animal> { Data = animal };
    }

    public async Task<ResultModel<Animal>> UpdateAsync(Animal entity){
        if (!await DoesAnimalIdExistsAsync(entity.Id)){
            return new ResultModel<Animal>
            {
                Errors = new List<string> { $"There is no animal with id {entity.Id}!" }
            };
        }
        _applicationDbContext.Animals.Update(entity);
        await _applicationDbContext.SaveChangesAsync();

        return new ResultModel<Animal>{
            Data = entity
        };
    }

    public async Task<ResultModel<Animal>> AddAsync(Animal entity){
        _applicationDbContext.Animals.Add(entity);
        await _applicationDbContext.SaveChangesAsync();
        return new ResultModel<Animal> { Data = entity };
    }

    public async Task<ResultModel<Animal>> DeleteAsync(Animal entity){
        _applicationDbContext.Animals.Remove(entity);
        await _applicationDbContext.SaveChangesAsync();
        return new ResultModel<Animal>
        {
            Data = entity
        };
    }

    public async Task<ResultModel<IEnumerable<Animal>>> GetByBreedIdAsync(Guid id){
        var animals = await _applicationDbContext.Animals
            .Include(a => a.Breed)
            .Where(a => a.BreedId.Equals(id)).ToListAsync();
        if (animals.Count == 0){
            return new ResultModel<IEnumerable<Animal>>
            {
                Errors = new List<string> { "Animal does not exist" }
            };
        }
        
        return new ResultModel<IEnumerable<Animal>> { Data = animals };
    }

    //It is possible to return an empty list, the species can have no animals at the moment
    public async Task<ResultModel<IEnumerable<Animal>>> GetBySpeciesIdAsync(Guid id){
        var animals = await _applicationDbContext.Animals
            .Include(a => a.Species)
            .Where(a => a.SpeciesId.Equals(id)).ToListAsync();
        if (animals.Count == 0){
            return new ResultModel<IEnumerable<Animal>>
            {
                Errors = new List<string> { "Animal does not exist" }
            };
        }
        return new ResultModel<IEnumerable<Animal>> { Data = animals };
    }
    
    //It is possible to return an empty list, the breed can have no animals at the moment
    public async Task<ResultModel<IEnumerable<Animal>>> SearchAsyncByBreedBySpecies(string search){
        search = search ?? string.Empty;
        var animals = await _applicationDbContext.Animals
            .Include(a => a.Breed)
            .Include(a => a.Species)
            .Where(a => a.Breed.Name
                            .Contains(search.Trim())
                        || a.Species.Name.Contains(search.Trim())).ToListAsync();
        if (animals.Count == 0){
            return new ResultModel<IEnumerable<Animal>>
            {
                Errors = new List<string> { "Animal does not exist" }
            };
        }
        return new ResultModel<IEnumerable<Animal>> { Data = animals };
    }

    public async Task<bool> DoesAnimalIdExistsAsync(Guid id){
        return await _applicationDbContext.Animals.AnyAsync(a => a.Id.Equals(id));
    }

    public Task<ResultModel<IEnumerable<User>>> GetUsersOfTheAnimal(Guid id){
        throw new NotImplementedException();
    }
   
  
    }
