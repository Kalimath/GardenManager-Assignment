using FluentValidation;
using InThePocket.Assignment.GardenManager.Application.Services.Garden;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;
using InThePocket.Assignment.GardenManager.Ports.Adapters.API.Controllers;
using Microsoft.Extensions.Logging;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class GardenControllerTestBase
{
    protected static readonly Guid SomeUserId = Guid.NewGuid();
    protected static readonly Guid SomeGardenId = Guid.NewGuid();

    protected static readonly GardenReference SomeGardenReference = new() { GardenId = SomeGardenId, UserId = SomeUserId };
    protected static readonly GardenDto SomeValidGardenDto = new()
    {
        GardenName = "some garden",
        TotalSurfaceArea = 100.1,
        LocationDescription = "some location description",
        TargetHumidityLevel = 55,
        UserId = SomeUserId
    };
    protected static readonly GardenDto SomeGardenDtoWithId = new()
    {
        GardenId = SomeGardenId,
        GardenName = "some garden",
        TotalSurfaceArea = 100.1,
        LocationDescription = "some location description",
        TargetHumidityLevel = 55,
        UserId = SomeUserId
    };
    
    protected readonly GardenController GardenController;
    protected readonly IValidator<GardenDto> GardenDtoValidator;
    protected readonly IGardenService GardenService;
    protected readonly ILogger<GardenController> Logger;

    protected GardenControllerTestBase()
    {
        GardenDtoValidator = Substitute.For<IValidator<GardenDto>>();
        GardenService = Substitute.For<IGardenService>();
        Logger = Substitute.For<ILogger<GardenController>>();
        
        GardenController = new GardenController(GardenDtoValidator, GardenService, Logger);
        
        //substitutes
        var validValidationResult = new FluentValidation.Results.ValidationResult();
        _ = GardenDtoValidator
            .ValidateAsync(Arg.Any<GardenDto>())
            .Returns(validValidationResult);
    }
}