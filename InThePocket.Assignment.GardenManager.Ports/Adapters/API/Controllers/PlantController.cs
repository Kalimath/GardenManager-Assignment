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
    IValidator<RealtimePlantMetricDataDto> rpmdValidator,
    IPlantService plantService,
    ILogger<PlantController> logger) : Controller, IPlantController
{
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Create(PlantDto plantDto)
    {
        var validationResult = await plantDtoValidator.ValidateAsync(plantDto);
        var rpmdValidationResult = await rpmdValidator.ValidateAsync(plantDto.RealtimePlantMetricData);

        if (!validationResult.IsValid || !rpmdValidationResult.IsValid)
        {
            var mergedErrors = validationResult.Errors.Concat(rpmdValidationResult.Errors).ToList();
            logger.LogError("Validation failed for plant creation: {errors}", mergedErrors);
            return BadRequest(mergedErrors.Select(failure => failure.ErrorMessage));
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
    
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var plants = await plantService.GetAllPlants();
            return Ok(plants);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error retrieving all plants");
            return Problem("An error occurred while retrieving all plants. "+ ex.Message);
        }
    }
    
    [HttpGet]
    [Route("Details")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> Get(Guid plantId)
    {
        try
        {
            var plants = await plantService.GetPlantById(plantId);
            return Ok(plants);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error retrieving all plants");
            return Problem("An error occurred while retrieving all plants. "+ ex.Message);
        }
    }
    
    [HttpPut]
    [ProducesResponseType(202)]
    public async Task<ActionResult> Update(PlantDto plantDto)
    {
        var validationResult = await plantDtoValidator.ValidateAsync(plantDto);
        var rpmdValidationResult = await rpmdValidator.ValidateAsync(plantDto.RealtimePlantMetricData);

        if (!validationResult.IsValid || !rpmdValidationResult.IsValid)
        {
            var mergedErrors = validationResult.Errors.Concat(rpmdValidationResult.Errors).ToList();
            logger.LogError("Validation failed for plant update: {errors}", mergedErrors);
            return BadRequest(mergedErrors.Select(failure => failure.ErrorMessage));
        }
        
        try
        {
            await plantService.UpdatePlant(plantDto);
            return Ok();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error updating plant {plantName} in garden {gardenId}", plantDto.PlantName, plantDto.GardenId);
            return Problem("An error occurred while updating the plant. "+ ex.Message);
        }
    }
    
    [HttpDelete]
    [ProducesResponseType(202)]
    public async Task<ActionResult> Delete(Guid plantId)
    {
        try
        {
            await plantService.RemovePlant(plantId);
            return Accepted();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error deleting plant {plantId}", plantId);
            return Problem("An error occurred while deleting the plant. "+ ex.Message);
        }
    }
}