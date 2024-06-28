using AnimalShelter_FuryTales.Consumer.Animals.Models;

namespace AnimalShelter_FuryTales.Consumer.Animals;

public interface IAnimalApiService{
    Task<AnimalResponseApiModel[]> GetAnimalsAsync();
    Task<AnimalResponseApiModel> GetAnimalByIdAsync(Guid id);
    Task CreateAnimalAsync(AnimalCreateRequestApiModel animalToCreate, string token);
    Task UpdateAnimalAsync(AnimalUpdateRequestApiModel animalToUpdate, string token);
    Task DeleteAnimalAsync(Guid id, string token);
}