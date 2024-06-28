namespace AnimalShelter_FuryTales.Core.Entities;

public class DonationItem{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid AnimalId { get; set; }
    public decimal Amount { get; set; }

    public virtual User User { get; set; }
    public virtual Animal Animal { get; set; }
}