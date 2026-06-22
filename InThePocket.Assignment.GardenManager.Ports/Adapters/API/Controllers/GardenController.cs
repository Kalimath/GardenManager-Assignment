using FluentValidation;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v1/[controller]")]
public class GardenController(IValidator<GardenDto> gardenDtoValidator, IGardenService gardenService) : ControllerBase, IGardenController
{

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> AddGarden(GardenDto gardenDto)
    {
        var validationResult = await gardenDtoValidator.ValidateAsync(gardenDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);
        
        await gardenService.AddGarden(gardenDto);
        return Created();
        
    }
}