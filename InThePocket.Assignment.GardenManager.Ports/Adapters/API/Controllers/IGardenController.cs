using InThePocket.Assignment.GardenManager.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

public interface IGardenController
{
    Task<ActionResult> AddGarden(GardenDto gardenDto);
}