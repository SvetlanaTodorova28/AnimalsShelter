using System.ComponentModel.DataAnnotations;

namespace AnimalShelter_FuryTales.Core.Entities;

public abstract class BaseEntity{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
}