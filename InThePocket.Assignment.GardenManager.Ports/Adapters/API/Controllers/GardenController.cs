using FluentValidation;
using InThePocket.Assignment.GardenManager.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

[ApiController]
public class GardenController(IValidator<GardenDto> gardenDtoValidator) : ControllerBase, IGardenController
{

    public async Task<ActionResult> AddGarden(GardenDto gardenDto)
    {
        _ = await gardenDtoValidator.ValidateAsync(gardenDto);

        return BadRequest();
    }
}