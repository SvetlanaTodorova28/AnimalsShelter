using System.Globalization;
using AnimalShelter_FuryTales.Client.Mvc;
using AnimalShelter_FuryTales.Client.Mvc.Services;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Animals;
using AnimalShelter_FuryTales.Consumer.Breeds;
using AnimalShelter_FuryTales.Consumer.DonationItems;
using AnimalShelter_FuryTales.Consumer.Enums;
using AnimalShelter_FuryTales.Consumer.Species;
using AnimalShelter_FuryTales.Consumer.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Scala.StockSimulation.Utilities.Authorization;
using Scala.StockSimulation.Utilities.Authorization.Interfaces;

var builder = WebApplication.CreateBuilder(args);
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IAnimalApiService, AnimalApiService>();
builder.Services.AddScoped<IBreedApiService, BreedApiService>();
builder.Services.AddScoped<ISpeciesApiService, SpeciesApiService>();
builder.Services.AddScoped<IUserAuthApiService, UserAuthApiService>();
builder.Services.AddScoped<IUserApiService, UserApiService>();
builder.Services.AddScoped<IEnumApiService, EnumApiService>();
builder.Services.AddScoped<IFormBuilder, FormBuilder>();
builder.Services.AddScoped<IDonationApiService, DonationApiService>();
builder.Services.AddScoped<IAuthorizationServiceDonations, AuthorizationServiceDonations>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options => {
    options.AddPolicy(GlobalConstants.AdminRoleName, policy => policy.RequireRole(GlobalConstants.AdminRoleName));
    options.AddPolicy(GlobalConstants.VolunteerRoleName, policy => policy.RequireRole(GlobalConstants.VolunteerRoleName));
    options.AddPolicy(GlobalConstants.HealthCarePolicy, policy => 
        policy.RequireClaim(GlobalConstants.HealthCareClaimType, new string[]{GlobalConstants.HealthCareClaimValue}));
    options.AddPolicy(GlobalConstants.DonationsPolicy, policy =>
        policy.RequireAssertion(context =>
        {
            var donationsTotalClaim = context.User.FindFirst(GlobalConstants.DonationClaimType);
            if (donationsTotalClaim != null && decimal.TryParse(donationsTotalClaim.Value, out decimal donationsTotal))
            {
                return donationsTotal >= GlobalConstants.DonationLevel300;
            }
            return false;
        }));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7224")
        .AllowAnyMethod()
        .AllowAnyHeader());


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
