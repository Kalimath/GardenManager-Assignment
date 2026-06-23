using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

public interface IGardenController
{
    Task<ActionResult> Create(GardenDto gardenDto);
    Task<ActionResult> Get([FromBody] GardenReference reference);
    Task<ActionResult> GetAll([FromBody] Guid userId);
    
}