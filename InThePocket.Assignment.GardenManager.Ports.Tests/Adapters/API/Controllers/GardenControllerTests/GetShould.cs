using FluentValidation.Results;
using InThePocket.Assignment.GardenManager.Contracts.Api;
using Microsoft.AspNetCore.Mvc;

namespace InThePocket.Assignment.GardenManager.Ports.Tests.Adapters.API.Controllers.GardenControllerTests;

public class GetShould : GardenControllerTestBase
{
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
    public async Task CallGardenService_WhenIdIsValid()
    {
        var someGardenId = Guid.NewGuid();
        
        _ = await GardenController.Get(SomeGardenReference);
        
        await GardenService
            .Received(1)
            .GetGardenByReference(SomeGardenReference);
    }
    
    [Fact]
    public async Task ReturnOkWithData_WhenIdIsValid()
    {
        var someGardenId = SomeGardenDtoWithId.GardenId;
        GardenService
            .GetGardenByReference(SomeGardenReference)
            .Returns(SomeGardenDtoWithId);
        
        var result = await GardenController.Get(SomeGardenReference);
        
        var response = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(SomeGardenDtoWithId, response.Value);
    }
}