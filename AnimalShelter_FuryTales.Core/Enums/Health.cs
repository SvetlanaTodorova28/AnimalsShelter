namespace AnimalShelter_FuryTales.Core.Enums;

public enum Health{
    Healthy,
    Minor_Illness,
    Major_Illness,
    Recovering,
    Chronic_Condition,
    Under_Observation
}

public static class AnimalHealthExtensions
{
    public static string ToText(this Health health){

        return health switch{
            Health.Healthy => "Healthy",
            Health.Minor_Illness => "Minor Illness",
            Health.Major_Illness => "Major Illness",
            Health.Recovering => "Recovering",
            Health.Chronic_Condition => "Chronic Condition",
            Health.Under_Observation => "Under Observation",
            _ => throw new NotImplementedException()
        };
        
}
}