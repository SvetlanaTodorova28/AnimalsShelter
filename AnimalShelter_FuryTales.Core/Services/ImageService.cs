using AnimalShelter_FuryTales.Core.Models;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace AnimalShelter_FuryTales.Core;

public class ImageService : IImageService{
    private readonly IHostEnvironment _hostEnvironment;

    public ImageService(IHostEnvironment hostEnvironment){
        _hostEnvironment = hostEnvironment;
    }

    public async Task<ResultModel<string>> AddOrUpdateImageAsync(IFormFile image, string filename = ""){
        if (string.IsNullOrEmpty(filename)){
            filename = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        }

        var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "img");
        if (!Directory.Exists(filePath)){
            Directory.CreateDirectory(filePath);
        }

        var fullPath = Path.Combine(filePath, filename);
        using (FileStream fs = new FileStream(fullPath, FileMode.Create)){
            try{
                await image.CopyToAsync(fs);
                return new ResultModel<string>{ Data = filename };
            }
            catch (FileNotFoundException fnfe){
                return new ResultModel<string>{ Errors = new List<string>{ fnfe.Message } };
            }
        }
      
    }
    public bool Delete(string fileName){
        var pathToImages = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "img");
        try{
            File.Delete(Path.Combine(pathToImages,fileName));
        }
        catch (FileNotFoundException fileNotFoundException){
            Console.WriteLine(fileNotFoundException.Message);
            return false;
        }

        return true;
    }
}


