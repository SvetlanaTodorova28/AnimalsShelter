namespace AnimalShelter_FuryTales.Core.Models;

public class BaseResult{
    
    public bool Success => !Errors.Any();
    public List<string> Errors { get; set; } = new List<string>();
}