using AnimalShelter_FuryTales.Consumer.Breeds.Models;
using AnimalShelter_FuryTales.Consumer.Species.Models;

namespace AnimalShelter_FuryTales.Consumer.Species;

public interface ISpeciesApiService{
    Task<SpeciesResponseApiModel[]> GetSpeciesAsync();
    Task<SpeciesResponseApiModel> GetSpeciesByIdAsync(Guid id);
    Task CreateSpeciesAsync(SpeciesCreateRequestApiModel speciesToCreate, string token);
    Task UpdateSpeciesAsync(SpeciesUpdateRequestApiModel speciesToUpdate, string token);
    Task DeleteSpeciesAsync(Guid id, string token);
}