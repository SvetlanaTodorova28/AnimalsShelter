using AnimalShelter_FuryTales.Core.Data;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter_FuryTales.Core;

public class BreedService:IBreedService{
    private readonly ApplicationDbContext _applicationDbContext;
    public BreedService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
     public IQueryable<Breed> GetAll(){
         return _applicationDbContext
             .Breeds;
     }

    public async Task<ResultModel<IEnumerable<Breed>>> ListAllAsync(){
        var breeds = await _applicationDbContext
            .Breeds
            .ToListAsync();
        var resultModel = new ResultModel<IEnumerable<Breed>>()
        {
            Data = breeds,
        };
        return resultModel;
    }

    public async Task<ResultModel<Breed>> GetByIdAsync(Guid id){
        var breed = await _applicationDbContext
            .Breeds
            .FirstOrDefaultAsync(b => b.Id == id);

        if(breed is null)
        {
            return new ResultModel<Breed>
            {
                Errors = new List<string> { "Breed does not exist" }
            };

        }
        return new ResultModel<Breed> { Data = breed };
    }

    public async Task<ResultModel<Breed>> UpdateAsync(Breed entity){
        if (!await DoesBreedIdExistAsync(entity.Id)){
            return new ResultModel<Breed>
            {
                Errors = new List<string> { $"There is no breed with id {entity.Id}!" }
            };
        }
        if (await DoesBreedNameExistsAsync(entity))
        {
            return new ResultModel<Breed>
            {
                Errors = new List<string>
                    { $"There is already a breed with name {entity.Name}!" }
            };

        }
        _applicationDbContext.Breeds.Update(entity);
        await _applicationDbContext.SaveChangesAsync();

        return new ResultModel<Breed>{
            Data = entity
        };
    }

    public async Task<ResultModel<Breed>> AddAsync(Breed entity){
        if (await DoesBreedNameExistsAsync(entity)){
            return new ResultModel<Breed>{
                Errors = new List<string>

                    { $"A breed with the name {entity.Name} already exists!" },

            };
        }
        _applicationDbContext.Breeds.Add(entity);
        await _applicationDbContext.SaveChangesAsync();
        return new ResultModel<Breed> { Data = entity };
    }

    public async Task<ResultModel<Breed>> DeleteAsync(Breed entity){
        if (await DoesBreedHasAnimal(entity)){
            return new ResultModel<Breed>{
                Errors = new List<string>
                    { $"Breed {entity.Name} has animals we can not remove !" }
            };
        }
        _applicationDbContext.Breeds.Remove(entity);
        await _applicationDbContext.SaveChangesAsync();
        return new ResultModel<Breed>
        {
            Data = entity
        };
    }
    
    public async Task<ResultModel<IEnumerable<Breed>>> GetByName(string search){
        var breeds = await _applicationDbContext.Breeds
            .Where(b => b.Name.Contains(search)).ToListAsync();
        return new ResultModel<IEnumerable<Breed>> { Data = breeds };
    }

    public async Task<bool> DoesBreedIdExistAsync(Guid id){
        return await _applicationDbContext.Breeds.AnyAsync(b => b.Id.Equals(id));
    }

    
    public async Task<bool> DoesBreedNameExistsAsync(Breed entity)
    {
        return await _applicationDbContext.Breeds
            .Where(b => b.Id != entity.Id)
            .AnyAsync(b => b.Name.Equals(entity.Name));
    }

    private async Task<bool> DoesBreedHasAnimal(Breed breed){
        bool breedHasAnimal = await _applicationDbContext.Animals
            .AnyAsync(animal => animal.BreedId.Equals(breed.Id));
        return breedHasAnimal;
    }

}