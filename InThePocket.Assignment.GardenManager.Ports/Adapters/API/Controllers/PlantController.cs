using FluentValidation;
using InThePocket.Assignment.GardenManager.Application.Services.Plant;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v1/[controller]")]
public class PlantController(
    IValidator<PlantDto> plantDtoValidator,
    IPlantService plantService,
    ILogger<PlantController> logger) : Controller, IPlantController
{
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Create(PlantDto plantDto)
    {
        var validationResult = await plantDtoValidator.ValidateAsync(plantDto);

        if (!validationResult.IsValid)
        {
            logger.LogError("Validation failed for plant creation: {errors}", validationResult.Errors);
            return BadRequest(validationResult);
        }
        
        try
        {
            await plantService.AddPlant(plantDto);
            return Created();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error adding plant {plantName} to garden {gardenId}", plantDto.PlantName, plantDto.GardenId);
            return Problem("An error occurred while adding the plant. "+ ex.Message);
        }
    }
}