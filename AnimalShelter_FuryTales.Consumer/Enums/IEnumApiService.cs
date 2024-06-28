namespace AnimalShelter_FuryTales.Consumer.Enums;

public interface IEnumApiService{
    Task<string[]> GetHealthAsync();
   
    Task<string[]> GetGenderAsync();
}