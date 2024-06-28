using AnimalShelter_FuryTales.Core.Enums;
namespace AnimalShelter_FuryTales.Core.Services;

public class EnumService:IEnumService{
    public async Task<IEnumerable<string>> GetHealthAsync(){
        var healthStatuses = Enum.GetValues(typeof(Health))
            .Cast<Health>()
            .Select(item => item.ToText());
        return healthStatuses;
    }


public async Task<IEnumerable<string>> GetGenderAsync(){
    var genderStatuses = Enum.GetValues(typeof(Gender))
        .Cast<Gender>()
        .Select(item => item.ToText());
    return genderStatuses;
    }
}