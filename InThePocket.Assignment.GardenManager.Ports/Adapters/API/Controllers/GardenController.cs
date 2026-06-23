using FluentValidation;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
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

    [HttpPost]
    [Route("Single")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Get([FromBody] GardenReference reference)
    {
        if (reference.GardenId.Equals(Guid.Empty)) return BadRequest("GardenId cannot be empty");
        if (reference.UserId.Equals(Guid.Empty)) return BadRequest("UserId cannot be empty");
        
        var requested = await gardenService.GetGardenByReference(reference);
        
        return Ok(requested);
    }

    [HttpPost]
    [Route("All")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<ActionResult> GetAll(Guid userId)
    {
        if (userId == Guid.Empty)
            return BadRequest("UserId cannot be empty");

        try
        {
            return Ok(await gardenService.GetGardensByUser(userId));
        }
        catch(Exception ex)
        {
            return BadRequest("Unable to retrieve gardens for the specified user");
        }
    }

    [HttpPut]
    [ProducesResponseType(202)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Update([FromBody] GardenDto updatedData)
    {
        var validationResult = await gardenDtoValidator.ValidateAsync(updatedData);

        if (!validationResult.IsValid) return BadRequest(validationResult);
        
        await gardenService.UpdateGarden(updatedData);
        
        return Accepted();
    }
}