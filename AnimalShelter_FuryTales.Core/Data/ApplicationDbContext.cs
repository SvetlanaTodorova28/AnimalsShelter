using AnimalShelter_FuryTales.Core.Data.Seeding;
using AnimalShelter_FuryTales.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter_FuryTales.Core.Data;

public class ApplicationDbContext:IdentityDbContext<User>{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
    
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<Species> Species { get; set; }
   
    public DbSet<Animal> Animals { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<DonationItem> DonationItems { get; set; }
   
    

    protected override void OnModelCreating(ModelBuilder modelBuilder){
       
        
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.Species)
            .WithMany() 
            .HasForeignKey(a => a.SpeciesId)
            .OnDelete(DeleteBehavior.Restrict); //


        modelBuilder.Entity<Animal>()
            .Property(a => a.MonthlyFoodExpenses)
            .HasPrecision(18, 2);
        Seeder.Seed(modelBuilder);
        
      
        base.OnModelCreating(modelBuilder);
    }
}