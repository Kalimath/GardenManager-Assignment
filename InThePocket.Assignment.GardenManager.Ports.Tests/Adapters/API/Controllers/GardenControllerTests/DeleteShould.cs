using FluentValidation.Results;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class DeleteShould : GardenControllerTestBase
{
    [Fact]
    public async Task CallGardenReferenceValidator()
    {
        _ = await GardenController.Delete(SomeGardenReference);
        
        await GardenReferenceValidator
            .Received(1)
            .ValidateAsync(SomeGardenReference);
    }
    
    [Fact]
    public async Task ReturnBadRequest_WhenGardenReferenceNotValid()
    {
        var invalidGardenReference = new GardenReference
        {
            GardenId = Guid.Empty,
            UserId = Guid.Empty
        };
        var validationResult = new ValidationResult
        {
            Errors = [
                new ValidationFailure {PropertyName = "GardenId"},
                new ValidationFailure {PropertyName = "UserId"}
            ]
        };
        GardenReferenceValidator
            .ValidateAsync(invalidGardenReference)
            .Returns(validationResult);
        
        var result = await GardenController.Delete(invalidGardenReference);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async Task CallGardenService_RemoveGarden()
    {
        _ = await GardenController.Delete(SomeGardenReference);
        
        await GardenService
            .Received(1)
            .RemoveGarden(SomeGardenReference);
    }
}