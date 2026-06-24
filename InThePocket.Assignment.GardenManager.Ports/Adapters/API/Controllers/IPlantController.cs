using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

public interface IPlantController
{
    Task<ActionResult> Create(PlantDto plantDto);
}