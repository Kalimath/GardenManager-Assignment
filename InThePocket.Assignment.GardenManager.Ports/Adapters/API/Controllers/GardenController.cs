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
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Create(GardenDto gardenDto)
    {
        var validationResult = await gardenDtoValidator.ValidateAsync(gardenDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);
        
        await gardenService.AddGarden(gardenDto);
        return Created();
        
    }

    [HttpGet]
    public async Task<ActionResult> Get([FromBody] Guid gardenId)
    {
        if (gardenId.Equals(Guid.Empty)) return BadRequest("GardenId cannot be empty");
        
        var requested = await gardenService.GetGardenById(gardenId);
        
        return Ok(requested);
    }
}