using InThePocket.Assignment.GardenManager.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

public interface IPlantController
{
    Task<ActionResult> Create(PlantDto plantDto);
}