using System.Security.Claims;
using AnimalShelter_FuryTales.Client.Mvc.Models;
using AnimalShelter_FuryTales.Client.Mvc.ViewModels;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Animals;
using AnimalShelter_FuryTales.Consumer.DonationItems;
using AnimalShelter_FuryTales.Consumer.Users;
using AnimalShelter_FuryTales.Consumer.Users.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace AnimalShelter_FuryTales.Client.Mvc.Controllers;


public class DonationItemsController : Controller{
    private readonly IUserApiService _userApiService;
    private readonly IAnimalApiService _animalApiService;
    private readonly IDonationApiService _donationApiService;
    

    public DonationItemsController(IUserApiService userApiService, IAnimalApiService animalApiService, IDonationApiService donationApiService){
        _userApiService = userApiService;
        _animalApiService = animalApiService;
        _donationApiService = donationApiService;
    }
    
    
    
    
    public async Task<IActionResult> Index()
    {
        var donationsFromApi = await _donationApiService.GetDonationsAsync();

        var animalIds = donationsFromApi.Select(d => d.AnimalId).Distinct();
        var animals = await Task.WhenAll(animalIds.Select(id => _animalApiService.GetAnimalByIdAsync(id)));
        var animalDictionary = animals.ToDictionary(a => a.Id, a => a);
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var donationsFromUser = donationsFromApi.Where(d => d.UserId == Guid.Parse(userId.Value)).ToList();
        var aggregatedDonations = donationsFromUser.GroupBy(d => d.AnimalId)
            .Select(group => new DonationItemsInfoViewModel
            {
                AnimalId = group.Key,
                AnimalName = animalDictionary[group.Key].Name,
                Quantity = group.Count(),
                Amount = group.Sum(d => d.Amount)
            }).ToList();

        var viewModel = new DonationItemsIndexViewModel
        {
            DonationCartItems = aggregatedDonations,
            TotalPrice = aggregatedDonations.Sum(d => d.Amount)
        };

        return View(viewModel);
    }




    
    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid animalId)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User not logged in.");
        }

        var user = await _userApiService.GetUserByIdAsync(Guid.Parse(userId));
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var animal = await _animalApiService.GetAnimalByIdAsync(animalId);
        if (animal == null)
        {
            return NotFound("Animal not found.");
        }

        var 
            donationItem = new DonationItemResponseApiModel{
                AnimalId = animalId,
                UserId = user.Id,
                Amount = animal.MonthlyFoodExpenses ?? 0,
            };
        
        await _donationApiService.CreateDonationItemAsync(donationItem, GlobalConstants.CookieToken);
        var itemsIndexViewModel = new DonationItemsIndexViewModel();
        itemsIndexViewModel.DonationCartItems = new List<DonationItemsInfoViewModel>();
       
        return RedirectToAction("Index", "DonationItems");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(Guid animalId) {
        var donationsFromApi = await _donationApiService.GetDonationsAsync();
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var donationsFromUser = donationsFromApi.Where(d => d.UserId == Guid.Parse(userId)).ToList();
        var toRemoveDonation = donationsFromUser.FirstOrDefault(d => d.AnimalId == animalId);
        if (!string.IsNullOrEmpty(userId)){
                await _donationApiService.DeleteDonationAsync(toRemoveDonation.Id, GlobalConstants.CookieToken);
        }
        TempData["success"] = "Donation deleted successfully";
        return RedirectToAction("Index", "DonationItems");
    }

    [HttpPost]
    public async Task<IActionResult> Checkout()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var totalPrice = await AddToDonationsOfUser();
        var userId = Guid.Parse(userIdClaim.Value);
    
        var userFromApi = new UsersUpdateDonationsApiModel()
        {
            Id = userId,
            Amount = totalPrice
        };

        await _userApiService.UpdateUserDonationsAsync(userFromApi, GlobalConstants.CookieToken);
        await DeleteDonations();
    
        // Check if the user now qualifies for a new claim
        var updatedUser = await _userApiService.GetUserByIdAsync(userId);

        var identity = User.Identity as ClaimsIdentity;
        if (identity == null)
        {
            return Unauthorized();
        }

        var donationClaim = identity.FindFirst(GlobalConstants.DonationClaimType);
        if (donationClaim != null)
        {
            identity.RemoveClaim(donationClaim);
        }

        if (updatedUser.TotalAmount >= 300) 
        {
            identity.AddClaim(new Claim(GlobalConstants.DonationClaimType, updatedUser.TotalAmount.ToString()));
        }

        // Refresh the user principal to update the cookie
        /*await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = true });*/
        
        TempData["success"] = "Thank you for your help!";
        return RedirectToAction("Index", "Home");
    }

    
    private async Task DeleteDonations(){
        var donationsFromApi = await _donationApiService.GetDonationsAsync();
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var donationsFromUser = donationsFromApi.Where(d => d.UserId == Guid.Parse(userId)).ToList();
        if (!string.IsNullOrEmpty(userId)){
            foreach (var donation in donationsFromUser){
                await _donationApiService.DeleteDonationAsync(donation.Id, GlobalConstants.CookieToken);
            }
        }
    }
    private async Task<decimal> AddToDonationsOfUser(){
        var donationsFromApi =  await _donationApiService.GetDonationsAsync();
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var donationsFromUser = donationsFromApi.Where(d => d.UserId == Guid.Parse(userId)).ToList();
        var total = 0.00M;
        if (!string.IsNullOrEmpty(userId)){
            foreach (var donation in donationsFromUser){
                total += donation.Amount;
            }
            return total;
        }
        return 0;
    }
    

}