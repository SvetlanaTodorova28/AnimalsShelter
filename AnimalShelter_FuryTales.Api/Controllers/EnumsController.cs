using AnimalShelter_FuryTales.Core.Enums;
using AnimalShelter_FuryTales.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Api.Controllers;

[Route("api/enums")]
public class EnumsController : Controller{
    private readonly IEnumService _enumService;
    public EnumsController(IEnumService enumService){
        _enumService = enumService;
    }
    [HttpGet("health")]
    public async Task<ActionResult> GetHealth(){
        
        var values = await _enumService.GetHealthAsync();
        return Ok(values);
    }
    [HttpGet("gender")]
    public async Task<ActionResult> Gender(){
        var values = await _enumService.GetGenderAsync();
        return Ok(values);
    }

}