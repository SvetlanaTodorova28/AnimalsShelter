using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;

namespace AnimalShelter_FuryTales.Core.Services;

public interface IAnimalService{
    IQueryable<Animal> GetAll();

    Task<ResultModel<IEnumerable<Animal>>> ListAllAsync();

    Task<ResultModel<Animal>> GetByIdAsync(Guid id);

    Task<ResultModel<Animal>> UpdateAsync(Animal entity);

    Task<ResultModel<Animal>> AddAsync(Animal entity);

    Task<ResultModel<Animal>> DeleteAsync(Animal entity);

    Task<ResultModel<IEnumerable<Animal>>> GetByBreedIdAsync(Guid id);
    
    Task<ResultModel<IEnumerable<Animal>>> GetBySpeciesIdAsync(Guid id);
    
    Task<ResultModel<IEnumerable<Animal>>> SearchAsyncByBreedBySpecies(string search);

    Task<bool> DoesAnimalIdExistsAsync(Guid id);

    Task<ResultModel<IEnumerable<User>>> GetUsersOfTheAnimal(Guid id);
}