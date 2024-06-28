using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter_FuryTales.Core.Data.Seeding;

public static class Seeder{
    public static void Seed(ModelBuilder modelBuilder){
      
        var species = new List<Species>
        {
            new Species { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Unknown" },
            new Species { Id = Guid.Parse("00000000-0000-0000-0000-000000000033"), Name = "Dog" },
            new Species { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Cat"},
            new Species { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "Donkey"},
            new Species { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Bunny"}
          
        };
       
        var breeds = new List<Breed>
        {
            new Breed { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Unknown",SpeciesId =species[0].Id },
            new Breed { Id = Guid.Parse("00000000-0000-0000-0000-000000000031"), Name = "Labrador",SpeciesId =species[1].Id },
            new Breed { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Stray Dog",SpeciesId =species[1].Id },
            new Breed { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "Stray Cat",SpeciesId =species[2].Id },
            new Breed { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "Anatolian",SpeciesId =species[3].Id },
            new Breed { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = "Hulstlander",SpeciesId =species[4].Id },
            
        };
        
        
       
        var animals = new List<Animal>
        {
            //1
            new Animal
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Name = "Layko",
                SpeciesId = species[1].Id,
                BreedId = breeds[1].Id,
                Age = 3,
                Gender = Gender.Male,
                Health = Health.Healthy,
                Description = "Friendly and energetic ",
                MonthlyFoodExpenses = 50.00M,
                Image = "Layko.jpg"
            },
            //2
            new Animal
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                Name = "Joro",
                SpeciesId = species[1].Id,
                BreedId = breeds[2].Id,
                Age = 3,
                Gender = Gender.Male,
                Health = Health.Minor_Illness,
                Description = "Friendly and energetic ",
                MonthlyFoodExpenses = 50.00M,
                Image = "Joro.jpg"
            },
            //3
            new Animal
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                Name = "Sony",
                SpeciesId = species[2].Id,
                BreedId = breeds[3].Id,
                Age = 2,
                Gender = Gender.Female,
                Health = Health.Healthy,
                Description = "Quiet and curious stray",
                MonthlyFoodExpenses = 45.00M,
                Image = "Sony.jpg"
            },
            //4
            new Animal
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Name = "Sonia",
                SpeciesId = species[3].Id,
                BreedId = breeds[4].Id,
                Age = 2,
                Gender = Gender.Female,
                Health = Health.Chronic_Condition,
                Description = "Gentle, Rescued, Burro",
                MonthlyFoodExpenses = 45.00M,
                Image = "Sonia.jpg"
            }, 
            //5
            new Animal{
                Id = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                Name = "Uchcho",
                SpeciesId = species[4].Id,
                BreedId = breeds[5].Id,
                Age = 1,
                Gender = Gender.Male,
                Health = Health.Healthy,
                Description = "This gentle, curious rabbit is eagerly waiting for a loving forever home",
                MonthlyFoodExpenses = 50.00M,
                Image = "Uchcho.jpg"
            },
            //6
            new Animal{
                Id = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                Name = "Pencho",
                SpeciesId = species[1].Id,
                BreedId = breeds[2].Id,
                Age = 11,
                Gender = Gender.Male,
                Health = Health.Major_Illness,
                Description = "This sweet, aging companion, despite his health challenges, still has plenty of love to give and is searching for a compassionate home to spend his golden years",
                MonthlyFoodExpenses = 150.00M,
                Image = "default.jpg"
            },
            //7
            new Animal{
                Id = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                Name = "Skokcho",
                SpeciesId = species[4].Id,
                BreedId = breeds[5].Id,
                Age = 4,
                Gender = Gender.Male,
                Health = Health.Healthy,
                Description = "Meet this delightful little bunny! Ready to hop right into your heart and home, this friendly companion promises years of joy and friendship",
                MonthlyFoodExpenses = 50.00M,
                Image = "Skokcho.jpg"
            },
            //8
            new Animal
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                Name = "Lyvcho",
                SpeciesId = species[2].Id,
                BreedId = breeds[3].Id,
                Age = 2,
                Gender = Gender.Female,
                Health = Health.Healthy,
                Description = "This radiant ginger cat is a bundle of energy and affection. Eager for a warm lap to curl up on",
                MonthlyFoodExpenses = 45.00M,
                Image = "Lyvcho.jpg"
            },
            
           
        };
        
        IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        
      
        // admin seeden
        var adminUser = new User{
            Id = GlobalConstants.AdminId,
            UserName = GlobalConstants.AdminUserName,
            FirstName = "Admin",
            Email = GlobalConstants.AdminUserName,
            NormalizedEmail = GlobalConstants.AdminUserName.ToUpper(),
            NormalizedUserName = GlobalConstants.AdminUserName.ToUpper(),
            EmailConfirmed = true,
            Gender = Gender.Female,
            SecurityStamp = "BABUNAPLANINAVHODCHETERI",
            ConcurrencyStamp = "4b277cc7-bcb0-4d91-8aab-08dc4b606f7a",
            Ability = "Administration",
            ProfilePicture = "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&top%5B%5D=dreads01,dreads02,frizzle,shortCurly,shortFlat,shortRound,shortWaved,sides,theCaesar,theCaesarAndSidePart&seed=Admin"
        };
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, GlobalConstants.AdminUserPassword);
        //volunteer  seeden
        var volunteers = new List<User>{
        new (){
            Id = "00000000-0000-0000-0000-000000000023",
            UserName = "penka@shelter.bg",
            NormalizedUserName = "PENKA@SHELTER.BG",
            Email = "penka@shelter.bg",
            NormalizedEmail = "PENKA@SHELTER.BG",
            EmailConfirmed = true,
            SecurityStamp = "DIFFERENTUNIQUESTRING",
            ConcurrencyStamp = "YETANOTHERUNIQUESTRING",
            FirstName = "Penka",
            LastName = "Petrova",
            Ability = GlobalConstants.HealthCareClaimValue,
            Gender = Gender.Female,
            ProfilePicture = "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&facialHairProbability=0&top%5B%5D=bigHair,bob,bun,curly,curvy,dreads,longButNotTooLong,shaggy,shavedSides,straightAndStrand,straight01,straight02&seed=penka"
            
        },
         new User
        {
            Id = "00000000-0000-0000-0000-000000000014",
            UserName = "mila@shelter.bg",
            NormalizedUserName = "MILA@SHELTER.BG",
            Email = "mila@shelter.bg",
            NormalizedEmail = "MILA@SHELTER.BG",
            EmailConfirmed = true,
            SecurityStamp = "DIFFERENTUNIQUESTRING",
            ConcurrencyStamp = "YETANOTHERUNIQUESTRING",
            FirstName = "Mila",
            LastName = "Nikolova",
            Ability = GlobalConstants.HealthCareClaimValue,
            Gender = Gender.Female,
            ProfilePicture = "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&facialHairProbability=0&top%5B%5D=bigHair,bob,bun,curvy,longButNotTooLong,shaggy,shaggyMullet,shavedSides,straightAndStrand,straight01,straight02&seed=mila"
            
            
        },
         new User
         {
             Id = "00000000-0000-0000-0000-000000000015",
             UserName = "ivan@shelter.bg",
             NormalizedUserName = "IVAN@SHELTER.BG",
             Email = "ivan@shelter.bg",
             NormalizedEmail = "IVAN@SHELTER.BG",
             EmailConfirmed = true,
             SecurityStamp = "DIFFERENTUNIQUESTRING",
             ConcurrencyStamp = "YETANOTHERUNIQUESTRING",
             FirstName = "Ivan",
             LastName = "Sybev",
             Ability = GlobalConstants.HealthCareClaimValue,
             Gender = Gender.Male,
             ProfilePicture = "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&top%5B%5D=dreads01,dreads02,frizzle,shortCurly,shortFlat,shortRound,shortWaved,sides,theCaesar,theCaesarAndSidePart&seed=ivan"

         },
         new User
         {
             Id = "00000000-0000-0000-0000-000000000035",
             UserName = "petyr@shelter.bg",
             NormalizedUserName = "PETYR@SHELTER.BG",
             Email = "petyr@shelter.bg",
             NormalizedEmail = "PETYR@SHELTER.BG",
             EmailConfirmed = true,
             SecurityStamp = "DIFFERENTUNIQUESTRING",
             ConcurrencyStamp = "YETANOTHERUNIQUESTRING",
             FirstName = "Petyr",
             LastName = "Stoqnov",
             Ability = "Cleaning",
             Gender = Gender.Male,
             ProfilePicture = "https://api.dicebear.com/5.x/avataaars/svg?mouth=default&top%5B%5D=dreads01,dreads02,frizzle,shortCurly,shortFlat,shortRound,shortWaved,sides,theCaesar,theCaesarAndSidePart&seed=petyr"

         }
        };
        var adopters = new List<User>{
            new(){
                Id = "00000000-0000-0000-0000-000000000024",
                UserName = "sarah@gmail.com",
                NormalizedUserName = "SARAH@GMAIL.COM",
                Email = "sarah@gmail.com",
                NormalizedEmail = "SARAH@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "DIFFERENTUNIQUESTRING",
                ConcurrencyStamp = "YETANOTHERUNIQUESTRING",
                FirstName = "Sarah",
                LastName = "Vrout",
                Ability = "",
                Gender = Gender.Female,
                ProfilePicture = GlobalConstants.DefaultAvatar
                   

            },
            new(){
                Id = "00000000-0000-0000-0000-000000000025",
                UserName = "tom@gmail.com",
                NormalizedUserName = "TOM@GMAIL.COM",
                Email = "tom@gmail.com",
                NormalizedEmail = "TOM@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "DIFFERENTUNIQUESTRING",
                ConcurrencyStamp = "YETANOTHERUNIQUESTRING",
                FirstName = "Tom",
                LastName = "Calme",
                Ability = "",
                Gender = Gender.Male,
                ProfilePicture = GlobalConstants.DefaultAvatar
            },
            
        };
        foreach(var volunteer in volunteers)
        {
            volunteer.PasswordHash = passwordHasher.HashPassword(volunteer, GlobalConstants.PasswordVolunteer);
            modelBuilder.Entity<User>().HasData(volunteer);
        }
        foreach(var adopter in adopters)
        {
            adopter.PasswordHash = passwordHasher.HashPassword(adopter, GlobalConstants.PasswordAdopter);
            modelBuilder.Entity<User>().HasData(adopter);
        }
        // Definieer de Admin role
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole{
            Id = GlobalConstants.AdminRoleId,
            Name = GlobalConstants.AdminRoleName,
            NormalizedName = GlobalConstants.AdminRoleName.ToUpper()
        });

        // Definieer de Volunteer role
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole{
            Id = GlobalConstants.VolunteerRoleId,
            Name = GlobalConstants.VolunteerRoleName,
            NormalizedName = GlobalConstants.VolunteerRoleName.ToUpper()
        });
        // Definieer de Adopter role
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole{
            Id = GlobalConstants.AdopterRoleId,
            Name = GlobalConstants.AdopterRoleName,
            NormalizedName = GlobalConstants.AdopterRoleName.ToUpper()
        });
        
        
        //full the database
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>{
                UserId = GlobalConstants.AdminId,
                RoleId = GlobalConstants.AdminRoleId
            },
            new IdentityUserRole<string> {
                UserId = volunteers[0].Id, 
                RoleId = GlobalConstants.VolunteerRoleId 
            },
            new IdentityUserRole<string> {
                UserId = volunteers[1].Id, 
                RoleId = GlobalConstants.VolunteerRoleId 
            },
            new IdentityUserRole<string> {
                UserId = volunteers[2].Id, 
                RoleId = GlobalConstants.VolunteerRoleId 
            },
            new IdentityUserRole<string> {
                UserId = adopters[0].Id, 
                RoleId = GlobalConstants.AdopterRoleId  
            },
            new IdentityUserRole<string> {
                UserId = adopters[1].Id, 
                RoleId = GlobalConstants.AdopterRoleId  
            }
        );

        var animalsUsers = new[]{
            new{
                UsersId = volunteers[0].Id,
                AnimalsId = animals[0].Id
            },
            new{
                UsersId = volunteers[0].Id,
                AnimalsId = animals[1].Id
            },
            new{
                UsersId = volunteers[1].Id,
                AnimalsId = animals[0].Id
            },
            new{
                UsersId = volunteers[1].Id,
                AnimalsId = animals[1].Id
            },
            new{
                UsersId = volunteers[2].Id,
                AnimalsId = animals[2].Id

            }
        };
        
        
        modelBuilder.Entity<Breed>().HasData(breeds);
        modelBuilder.Entity<Species>().HasData(species);
        modelBuilder.Entity<User>().HasData(adminUser);
        modelBuilder.Entity<Animal>().HasData(animals);
        modelBuilder
            .Entity($"{nameof(Animal)}{nameof(User)}")
            .HasData(animalsUsers);
        
       

        
        
        
      


    }
}