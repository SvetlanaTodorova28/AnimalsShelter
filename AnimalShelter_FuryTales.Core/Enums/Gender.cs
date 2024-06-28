namespace AnimalShelter_FuryTales.Core.Enums;

public enum Gender{
    Male,
    Female,
    Unknown
}

public static class AnimalsGenderExtensions
    {
    public static string ToText(this Gender gender){

        return gender switch{
            Gender.Male => "Male",
            Gender.Female => "Female",
            Gender.Unknown => "Unknown"
        };

    }
}