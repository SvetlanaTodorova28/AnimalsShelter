using System.Net.Http.Headers;
using System.Net.Http.Json;
using AnimalShelter_FuryTales.Constants;
using AnimalShelter_FuryTales.Consumer.Animals.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AnimalShelter_FuryTales.Consumer.Animals;

public class AnimalApiService:IAnimalApiService{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _shelterApiClient;
    public AnimalApiService(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
        _shelterApiClient = _httpClientFactory.CreateClient(GlobalConstants.HttpClient);
        _shelterApiClient.BaseAddress = new Uri(ApiRoutes.Animals);
    }
    
    public async Task<AnimalResponseApiModel[]> GetAnimalsAsync(){
        var animals = await _shelterApiClient.GetFromJsonAsync<AnimalResponseApiModel[]>("");
        if (animals is not null) {
            return animals;
        }

        return Array.Empty<AnimalResponseApiModel>();
    }

    public async Task<AnimalResponseApiModel> GetAnimalByIdAsync(Guid id){
        var animal = await _shelterApiClient.GetFromJsonAsync<AnimalResponseApiModel>($"{id}");
        return animal;
    }

    
    public async Task CreateAnimalAsync(AnimalCreateRequestApiModel animalToCreate, string token){
        _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        HttpResponseMessage responseMessage;
        if (animalToCreate.Image != null){
            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(animalToCreate.Name ?? string.Empty), nameof(animalToCreate.Name));
            formData.Add(new StringContent(animalToCreate.SpeciesId.ToString()), nameof(animalToCreate.SpeciesId));
            formData.Add(new StringContent(animalToCreate.BreedId.ToString()), nameof(animalToCreate.BreedId));
            formData.Add(new StringContent(animalToCreate.Age.ToString()), nameof(animalToCreate.Age));
            formData.Add(new StringContent(animalToCreate.Gender ?? string.Empty), nameof(animalToCreate.Gender));
            formData.Add(new StringContent(animalToCreate.Health ?? string.Empty), nameof(animalToCreate.Health));
            formData.Add(new StringContent(animalToCreate.Description ?? string.Empty), nameof(animalToCreate.Description));
            formData.Add(new StringContent(animalToCreate.Donation.ToString()), nameof(animalToCreate.Donation));
            if (animalToCreate.UsersIds != null)
            {
                foreach (var userId in animalToCreate.UsersIds)
                {
                    formData.Add(new StringContent(userId.ToString()), "UsersIds");
                }
            }
            using var imageStream = animalToCreate.Image.OpenReadStream();
            var imageContent = new StreamContent(imageStream);
            var mimeType = GetMimeType(animalToCreate.Image.FileName);
            imageContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);  
            formData.Add(imageContent, "Image", animalToCreate.Image.FileName);
            responseMessage  = await _shelterApiClient.PostAsync("add-with-image", formData);
        }
        else{
            responseMessage = await _shelterApiClient.PostAsJsonAsync("", animalToCreate);
        }
        
    }

    private string GetMimeType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        switch (extension)
        {
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";
            case ".png":
                return "image/png";
            default:
                throw new ArgumentException("Unsupported file type");
        }
    }

  public async Task UpdateAnimalAsync(AnimalUpdateRequestApiModel animalToUpdate, string token)
{
    _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    HttpResponseMessage responseMessage;
    if (animalToUpdate.Image != null){
        using var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(animalToUpdate.Id.ToString()), nameof(animalToUpdate.Id));
        formData.Add(new StringContent(animalToUpdate.Name), nameof(animalToUpdate.Name));
        formData.Add(new StringContent(animalToUpdate.SpeciesId.ToString()), nameof(animalToUpdate.SpeciesId));
        formData.Add(new StringContent(animalToUpdate.BreedId.ToString()), nameof(animalToUpdate.BreedId));
        formData.Add(new StringContent(animalToUpdate.Age.ToString()), nameof(animalToUpdate.Age));
        formData.Add(new StringContent(animalToUpdate.Gender), nameof(animalToUpdate.Gender));
        formData.Add(new StringContent(animalToUpdate.Health), nameof(animalToUpdate.Health));
        formData.Add(new StringContent(animalToUpdate.Description), nameof(animalToUpdate.Description));
        formData.Add(new StringContent(animalToUpdate.Donation.ToString()), nameof(animalToUpdate.Donation));

        if (animalToUpdate.UsersIds != null){
            foreach (var userId in animalToUpdate.UsersIds){
                formData.Add(new StringContent(userId.ToString()), "UsersIds");
            }
        }

        using var imageStream = animalToUpdate.Image.OpenReadStream();
        var imageContent = new StreamContent(imageStream);
        var mimeType = GetMimeType(animalToUpdate.Image.FileName);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
        formData.Add(imageContent, "Image", animalToUpdate.Image.FileName);
        responseMessage = await _shelterApiClient.PutAsync("update-with-image", formData);
    }
    else{
        responseMessage = await _shelterApiClient.PutAsJsonAsync("", animalToUpdate);
    }

    if (!responseMessage.IsSuccessStatusCode)
    {
        var errorResponse = await responseMessage.Content.ReadAsStringAsync();
        throw new ApplicationException($"Failed to update animal: {errorResponse}");
    }
}
  


    public async Task DeleteAnimalAsync(Guid id, string token){
            _shelterApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _shelterApiClient.DeleteAsync($"{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
            
                throw new ApplicationException($"Failed to delete animal: {errorResponse}");
            }
        }
    
}