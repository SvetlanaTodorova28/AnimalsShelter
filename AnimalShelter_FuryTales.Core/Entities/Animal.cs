using System.ComponentModel.DataAnnotations;
using AnimalShelter_FuryTales.Core.Enums;

namespace AnimalShelter_FuryTales.Core.Entities;

public class Animal:BaseEntity{
    public Species Species { get; set; }
   
    public Guid SpeciesId { get; set; }
    public Breed Breed { get; set; }
    
    public Guid BreedId { get; set; }
    public int? Age { get; set; }
    public Gender Gender { get; set; }
    public Health  Health { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public decimal? MonthlyFoodExpenses { get; set; }
    public virtual ICollection<User>? Users { get; set; }
    
}