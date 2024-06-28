using AnimalShelter_FuryTales.Core.Data;
using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter_FuryTales.Core.Services;

public class DonationItemService : IDonationItemService
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public DonationItemService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public IQueryable<DonationItem> GetAll(){
        return _applicationDbContext
            .DonationItems;
    }

    public async Task<ResultModel<IEnumerable<DonationItem>>> ListAllAsync(){
        var donations = await _applicationDbContext
            .DonationItems
            .ToListAsync();
        var resultModel = new ResultModel<IEnumerable<DonationItem>>()
        {
            Data = donations
        };
        return resultModel;
    }
    public async Task<ResultModel<DonationItem>> AddAsync(DonationItem entity){
        _applicationDbContext.DonationItems.Add(entity);
        await _applicationDbContext.SaveChangesAsync();
        return new ResultModel<DonationItem> { Data = entity };
    }
    public async Task<ResultModel<DonationItem>> GetByIdAsync(Guid id){
        var donationItem = await _applicationDbContext
            .DonationItems
            .FirstOrDefaultAsync(di => di.Id == id);

        if(donationItem is null)
        {
            return new ResultModel<DonationItem>
            {
                Errors = new List<string> { "Donation does not exist" }
            };

        }
        return new ResultModel<DonationItem> { Data = donationItem };
    }
    
    public async Task<ResultModel<DonationItem>> DeleteAsync(DonationItem entity){
       
       
        _applicationDbContext.DonationItems.Remove(entity);
        await _applicationDbContext.SaveChangesAsync();
        return new ResultModel<DonationItem>
        {
            Data = entity
        };
    }
    
    public async Task<ResultModel<IEnumerable<DonationItem>>> GetByAnimalIdAsync(Guid id){
        var donatonsForAnimalWithId = await _applicationDbContext.DonationItems
            .Where(di => di.AnimalId.Equals(id)).ToListAsync();
        if (donatonsForAnimalWithId.Count == 0){
            return new ResultModel<IEnumerable<DonationItem>>
            {
                Errors = new List<string> { "Animal does not exist" }
            };
        }
        
        return new ResultModel<IEnumerable<DonationItem>> { Data = donatonsForAnimalWithId };
    }
}
