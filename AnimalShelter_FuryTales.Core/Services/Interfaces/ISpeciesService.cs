using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;

namespace AnimalShelter_FuryTales.Core.Services;

public interface ISpeciesService{
    IQueryable<Species> GetAll();

    Task<ResultModel<IEnumerable<Species>>> ListAllAsync();

    Task<ResultModel<Species>> GetByIdAsync(Guid id);

    Task<ResultModel<Species>> UpdateAsync(Species entity);

    Task<ResultModel<Species>> AddAsync(Species entity);

    Task<ResultModel<Species>> DeleteAsync(Species entity);

    Task<ResultModel<IEnumerable<Species>>> GetByName(string search);

    Task<bool> DoesSpeciesIdExistAsync(Guid id);

    Task<bool> DoesSpeciesNameExistsAsync(Species entity);
}