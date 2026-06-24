using FluentValidation;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v1/[controller]")]
public class GardenController(
    IValidator<GardenDto> gardenDtoValidator, 
    IValidator<GardenReference> gardenReferenceValidator,
    IGardenService gardenService,
    ILogger<GardenController> logger) : ControllerBase, IGardenController
{

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult> Create(GardenDto gardenDto)
    {
        var validationResult = await gardenDtoValidator.ValidateAsync(gardenDto);

        if (!validationResult.IsValid)
        {
            logger.LogError("Validation failed for garden creation: {errors}", validationResult.Errors);
            return BadRequest(validationResult);
        }

        try
        {
            await gardenService.AddGarden(gardenDto);
            return Created();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error creating garden for user {userId}", gardenDto.UserId);
            return Problem("An error occurred while creating the garden.");
        }
    }

    [HttpPost]
    [Route("Details")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> Get([FromBody] GardenReference reference)
    {
        var validationResult = await gardenReferenceValidator.ValidateAsync(reference);
        
        if (!validationResult.IsValid)
        {
            logger.LogError("Validation failed for garden retrieval: {errors}", validationResult.Errors);
            return BadRequest("Invalid garden reference provided");
        }

        var requested = await gardenService.GetGardenByReference(reference);
        
        return Ok(requested);
    }

    [HttpGet]
    [Route("All")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> GetAll(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            logger.LogError("UserId is empty in the {endpoint} request", nameof(GetAll));
            return BadRequest("UserId cannot be empty");
        }

        try
        {
            return Ok(await gardenService.GetGardensByUser(userId));
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error retrieving gardens for user {userId}", userId);
            return Problem("Unable to retrieve gardens for the specified user");
        }
    }

    [HttpPut]
    [ProducesResponseType(202)]
    public async Task<ActionResult> Update([FromBody] GardenDto updatedData)
    {
        var validationResult = await gardenDtoValidator.ValidateAsync(updatedData);

        if (!validationResult.IsValid)
        {
            logger.LogError("Validation failed for garden update: {errors}", validationResult.Errors);
            return BadRequest(validationResult);
        }

        try
        {
            await gardenService.UpdateGarden(updatedData);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating garden with ID {gardenId}", updatedData.GardenId);
            return Problem("An error occurred while updating the garden.");
        }
        
        return Accepted();
    }

    [HttpDelete]
    [ProducesResponseType(202)]
    public async Task<ActionResult> Delete(GardenReference reference)
    {
        var validationResult = await gardenReferenceValidator.ValidateAsync(reference);
        
        if (!validationResult.IsValid)
        {
            logger.LogError("Validation failed for garden removal: {errors}", validationResult.Errors);
            return BadRequest("Invalid garden reference provided");
        }
        
        try
        {
            await gardenService.RemoveGarden(reference);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error removing garden with ID {gardenId}", reference.GardenId);
            return Problem("An error occurred while removing the garden.");
        }
        
        return Accepted();
    }
}