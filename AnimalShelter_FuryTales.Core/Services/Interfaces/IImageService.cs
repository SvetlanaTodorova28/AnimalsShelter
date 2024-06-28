using AnimalShelter_FuryTales.Core.Models;
using Microsoft.AspNetCore.Http;

namespace AnimalShelter_FuryTales.Core.Services;

public interface IImageService{
    Task<ResultModel<string>> AddOrUpdateImageAsync(IFormFile image, string filename="");
    bool Delete(string fileName);

}