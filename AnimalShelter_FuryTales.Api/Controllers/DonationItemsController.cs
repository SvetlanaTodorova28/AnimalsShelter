using AnimalShelter_FuryTales.Api.Dtos.DonationItems;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Api.Controllers;

[Route("api/[controller]")]

public class DonationItemsController:ControllerBase{
    private readonly IDonationItemService _donationItemService;

    public DonationItemsController(IDonationItemService donationItemService){
        _donationItemService = donationItemService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(){
        var result = await _donationItemService.ListAllAsync();
        if (result.Success){
            var donationsDtos = result
                .Data
                .Select(x => new DonationItemResponseDto(){
                    Id = x.Id,
                  UserId = x.UserId,
                  AnimalId = x.AnimalId,
                  Amount = x.Amount
                });
            return Ok(donationsDtos);
        }

        return BadRequest(result.Errors);
    }
    [HttpPost]
    public async Task<IActionResult> AddDonationItem([FromBody]DonationItemResponseDto donationItemDto){
        
        //create donation item
        var donationItem = new DonationItem(){
            UserId = Guid.Parse(donationItemDto.UserId.ToString()),
            AnimalId = Guid.Parse(donationItemDto.AnimalId.ToString()),
            Amount = donationItemDto.Amount
        };
        
        
        var result = await _donationItemService.AddAsync(donationItem);
          
        if(result.Success){
            var donationDto = new DonationItemResponseDto(){
                Id = result.Data.Id,
                UserId = result.Data.UserId,
                AnimalId = result.Data.AnimalId,
                Amount = result.Data.Amount
            };
            return CreatedAtAction(nameof(Get), new {id = result.Data.Id}, donationDto);
        }
        return BadRequest(result.Errors);
    
    }

    [HttpDelete("{id}")]
   
    public async Task<IActionResult> Delete(Guid id){
        var toRemoveDonation = await _donationItemService.GetByIdAsync(id);
        var result = await _donationItemService.DeleteAsync(toRemoveDonation.Data);
        if (result.Success)
        {
            return Ok($"Donation with id : {toRemoveDonation} deleted");
        }
        return BadRequest(result.Errors);
    }
}