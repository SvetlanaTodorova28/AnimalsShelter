namespace AnimalShelter_FuryTales.Core.Services;

public interface IEnumService{
    Task<IEnumerable<string>> GetHealthAsync();
    Task<IEnumerable<string>> GetGenderAsync();
    

}