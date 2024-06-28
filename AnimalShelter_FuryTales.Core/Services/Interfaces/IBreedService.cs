using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;

namespace AnimalShelter_FuryTales.Core.Services;

public interface IBreedService{
    IQueryable<Breed> GetAll();

    Task<ResultModel<IEnumerable<Breed>>> ListAllAsync();

    Task<ResultModel<Breed>> GetByIdAsync(Guid id);

    Task<ResultModel<Breed>> UpdateAsync(Breed entity);

    Task<ResultModel<Breed>> AddAsync(Breed entity);

    Task<ResultModel<Breed>> DeleteAsync(Breed entity);
    

    Task<ResultModel<IEnumerable<Breed>>> GetByName(string search);

    Task<bool> DoesBreedIdExistAsync(Guid id);

    Task<bool> DoesBreedNameExistsAsync(Breed entity);
}