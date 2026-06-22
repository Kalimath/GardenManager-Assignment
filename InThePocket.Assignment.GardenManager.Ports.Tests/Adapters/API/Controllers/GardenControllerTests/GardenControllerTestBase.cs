using FluentValidation;
using InThePocket.Assignment.GardenManager.Contracts.Dto;
using InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class GardenControllerTestBase
{
    protected static readonly GardenDto ValidGardenDto = new()
    {
        GardenName = "some garden",
        TotalSurfaceArea = 100.1,
        LocationDescription = "some location description"
    };
    
    protected readonly GardenController GardenController;
    protected readonly IValidator<GardenDto> GardenDtoValidator;

    protected GardenControllerTestBase()
    {
        GardenDtoValidator = Substitute.For<IValidator<GardenDto>>();
        
        GardenController = new GardenController(GardenDtoValidator);
    }
}