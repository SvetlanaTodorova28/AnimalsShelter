using AnimalShelter_FuryTales.Core.Data;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter_FuryTales.Core.Services;

public class SpeciesService:ISpeciesService{
    private readonly ApplicationDbContext _applicationDbContext;
    public SpeciesService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public IQueryable<Species> GetAll(){
        return _applicationDbContext
            .Species
            .Include(s=>s.Breeds);
    }

    public async Task<ResultModel<IEnumerable<Species>>> ListAllAsync(){
        var species = await _applicationDbContext
            .Species
            .Include(s => s.Breeds)
            .ToListAsync();
        var resultModel = new ResultModel<IEnumerable<Species>>()
        {
            Data = species,
        };
        return resultModel;
    }

    public async Task<ResultModel<Species>> GetByIdAsync(Guid id){
        var species = await _applicationDbContext
            .Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == id);

        if(species is null)
        {
            return new ResultModel<Species>
            {
                Errors = new List<string> { "Species does not exist" }
            };

        }
        return new ResultModel<Species> { Data = species };
    }

    public async Task<ResultModel<Species>> UpdateAsync(Species entity){
        if (!await DoesSpeciesIdExistAsync(entity.Id)){
            return new ResultModel<Species>
            {
                Errors = new List<string> { $"There is no breed with id {entity.Id}!" }
            };
        }
        if (await DoesSpeciesNameExistsAsync(entity))
        {
            return new ResultModel<Species>
            {
                Errors = new List<string>
                    { $"There is already a breed with name {entity.Name}!" }
            };

        }
        _applicationDbContext.Species.Update(entity);
        await _applicationDbContext.SaveChangesAsync();

        return new ResultModel<Species>{
            Data = entity
        };
    }

    public async Task<ResultModel<Species>> AddAsync(Species entity){
        if (await DoesSpeciesNameExistsAsync(entity)){
            return new ResultModel<Species>{
                Errors = new List<string>

                    { $"A breed with the name {entity.Name} already exists!" },

            };
        }
        _applicationDbContext.Species.Add(entity);
        await _applicationDbContext.SaveChangesAsync();
        return new ResultModel<Species> { Data = entity };
    }

    public async Task<ResultModel<Species>> DeleteAsync(Species entity){
        if (await DoesSpeciesHasAnimals(entity)){
            return new ResultModel<Species>{
                Errors = new List<string>
                    { $"Species {entity.Name} has animals we can not remove !" }
            };
        }
        _applicationDbContext.Species.Remove(entity);
        await _applicationDbContext.SaveChangesAsync();
        return new ResultModel<Species>
        {
            Data = entity
        };
    }
    

    public async Task<ResultModel<IEnumerable<Species>>> GetByName(string search){
        var species = await _applicationDbContext.Species
            .Where(s => s.Name.Contains(search)).ToListAsync();
        return new ResultModel<IEnumerable<Species>> { Data = species };
    }

    public async Task<bool> DoesSpeciesIdExistAsync(Guid id){
        return await _applicationDbContext.Species.AnyAsync(s => s.Id.Equals(id));
    }

    public async  Task<bool> DoesSpeciesNameExistsAsync(Species entity){
        return await _applicationDbContext.Species
            .Where(s => s.Id != entity.Id)
            .AnyAsync(s => s.Name.Equals(entity.Name));
    }
    public async Task<bool> DoesSpeciesHasAnimals(Species species){
        bool speciesHasAnimal = await _applicationDbContext.Animals
            .AnyAsync(animal => animal.SpeciesId.Equals(species.Id));
        return speciesHasAnimal;
    }
}