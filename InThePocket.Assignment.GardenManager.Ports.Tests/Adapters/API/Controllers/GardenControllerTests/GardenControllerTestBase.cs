using FluentValidation;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Contracts.Dto;
using InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class GardenControllerTestBase
{
    protected static readonly GardenDto ValidGardenDto = new()
    {
        GardenName = "some garden",
        TotalSurfaceArea = 100.1,
        LocationDescription = "some location description",
        TargetHumidityLevel = 55
    };
    
    protected readonly GardenController GardenController;
    protected readonly IValidator<GardenDto> GardenDtoValidator;
    protected readonly IGardenService GardenService;

    protected GardenControllerTestBase()
    {
        GardenDtoValidator = Substitute.For<IValidator<GardenDto>>();
        GardenService = Substitute.For<IGardenService>();
        
        GardenController = new GardenController(GardenDtoValidator, GardenService);
        
        //substitutes
        var validValidationResult = new FluentValidation.Results.ValidationResult();
        _ = GardenDtoValidator
            .ValidateAsync(Arg.Any<GardenDto>())
            .Returns(validValidationResult);
    }
}