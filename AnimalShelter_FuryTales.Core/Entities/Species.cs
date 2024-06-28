namespace AnimalShelter_FuryTales.Core.Entities;

public class Species:BaseEntity{
   
    public ICollection<Breed>? Breeds { get; set; }
}