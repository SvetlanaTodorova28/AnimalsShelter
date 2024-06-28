using AnimalShelter_FuryTales.Consumer.Animals.Models;
using AnimalShelter_FuryTales.Consumer.Breeds.Models;

namespace AnimalShelter_FuryTales.Consumer.Breeds;

public interface IBreedApiService{
    Task<BreedResponseApiModel[]> GetBreedsAsync();
    Task<BreedResponseApiModel> GetBreedByIdAsync(Guid id);
    Task<AnimalResponseApiModel[]> GetAnimalsByBreedIdAsync(Guid id);
    Task CreateBreedAsync(BreedCreateRequestApiModel breedToCreate, string token);
    Task UpdateBreedAsync(BreedUpdateRequestApiModel breedToUpdate, string token);
    Task DeleteBreedAsync(Guid id, string token);
}