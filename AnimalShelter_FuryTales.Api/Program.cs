using System.Configuration;
using System.Globalization;
using System.Text;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Constants.Settings;
using AnimalShelter_FuryTales.Core;
using AnimalShelter_FuryTales.Core.Data;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scala.StockSimulation.Utilities.Authorization;
using Scala.StockSimulation.Utilities.Authorization.Interfaces;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
// Add services to the container.
builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<ISpeciesService, SpeciesService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IClaimService, ClaimsService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IEnumService, EnumService>();
builder.Services.AddScoped<IAvatarService, AvatarService>();
builder.Services.AddScoped<IEnumService, EnumService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IDonationItemService, DonationItemService>();
builder.Services.AddScoped<IAuthorizationServiceDonations, AuthorizationServiceDonations>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Configureer Swagger om de Authorization input te gebruiken
    //Bij toevoegen van token in Swagger UI :"Bearer {token}"
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("FuryTalesDb"));
});

builder.Services.AddIdentity<User, IdentityRole>(options => {
        //only for testing purposes
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 4;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt => {
    opt.TokenLifespan = TimeSpan.FromDays(1);
});
builder.Services.AddControllers();
   //add authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWTConfiguration:Issuer"],
        ValidAudience = builder.Configuration["JWTConfiguration:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfiguration:SigningKey"]))
    };
});
//add Authorization
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
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.InjectJavascript("/custom-swagger.js");
    });
}
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();