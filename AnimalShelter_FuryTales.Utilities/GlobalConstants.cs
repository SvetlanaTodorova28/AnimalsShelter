namespace AnimalShelter_FuryTales.Constants;



public static class GlobalConstants{
    public const string AdminRoleId = "00000000-0000-0000-0000-000000000020";
    public const string AdminId = "00000000-0000-0000-0000-000000000022"; 
    public const string AdminRoleName = "Admin";
    public const string AdminUserName = "Admin@shelter.bg";
    public const string AdminUserPassword = "Admin1234";
       
    public const string VolunteerRoleId = "00000000-0000-0000-0000-000000000021";
    public const string VolunteerRoleName = "Volunteer";
    public const string PasswordVolunteer = "Volunteer1234";
        
    public const string AdopterRoleId = "00000000-0000-0000-0000-000000000027";
    public const string AdopterRoleName = "Adopter";
    public const string PasswordAdopter = "Adopter1234";
    
    public const string HealthCareClaimType = "AnimalCare";
    public const string HealthCareClaimValue = "Healthcare course";
    public const string HealthCarePolicy = "VolunteerWithAnimalCare";
    
    public const string DonationClaimType = "DonationLevel300";
    public const string DonationsPolicy = "RequireHighDonationLevel";
    public const decimal DonationLevel300 = 300;
   
    
    
    public const string CookieToken = "jwtToken";
    public const string DefaultAvatar = "https://api.dicebear.com/8.x/bottts/svg?seed=Sammy";
    
    public const string IndexVolunteers = "IndexVolunteers";
    public const string IndexAdopters = "IndexAdopters";
    
    public const string HttpClient = "ShelterApiClient";
}