using AnimalShelter_FuryTales.Core.Entities;
using AnimalShelter_FuryTales.Core.Models;

namespace AnimalShelter_FuryTales.Core.Services;

public interface IDonationItemService{
    IQueryable<DonationItem> GetAll();
    Task<ResultModel<IEnumerable<DonationItem>>> ListAllAsync();
    Task<ResultModel<DonationItem>> GetByIdAsync(Guid id);
    Task<ResultModel<IEnumerable<DonationItem>>> GetByAnimalIdAsync(Guid id);
    Task<ResultModel<DonationItem>> AddAsync(DonationItem entity);
     Task<ResultModel<DonationItem>> DeleteAsync(DonationItem entity);
}