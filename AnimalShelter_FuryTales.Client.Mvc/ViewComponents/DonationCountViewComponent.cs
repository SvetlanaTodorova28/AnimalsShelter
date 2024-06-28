using System.Security.Claims;

using AnimalShelter_FuryTales.Consumer.DonationItems;
using AnimalShelter_FuryTales.Consumer.Users;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.ViewComponents;

public class DonationCountViewComponent : ViewComponent{

    private readonly IUserApiService _userApiService;
    private readonly IDonationApiService _donationApiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

        public DonationCountViewComponent(IDonationApiService donationApiService, IUserApiService userApiService, IHttpContextAccessor httpContextAccessor)
        {
            _donationApiService = donationApiService;
            _userApiService = userApiService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var donationCount = 0;

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                donationCount = await GetDonationCountForUser(userId);
            }

            return View(donationCount);
        }

        private async Task<int> GetDonationCountForUser(string userId){
            var donationsFromApi = await _donationApiService.GetDonationsAsync();
            var donationsFromUser = donationsFromApi
                .Where(d => d.UserId == Guid.Parse(userId))
                .ToList().Count;
            return  donationsFromUser;
        }

}

