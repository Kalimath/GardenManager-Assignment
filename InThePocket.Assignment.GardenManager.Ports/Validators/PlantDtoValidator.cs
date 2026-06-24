using FluentValidation;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Ports.Validators;

public sealed class PlantDtoValidator : AbstractValidator<PlantDto>
{
    public PlantDtoValidator()
    {
        RuleFor(p => p.PlantName).NotEmpty().WithMessage("Plant name must not be empty.");
        RuleFor(p => p.Species).NotEmpty().WithMessage("Species must not be empty.");
        RuleFor(p => p.PlantType).IsInEnum().WithMessage("Plant type must be a valid enum value.");
        RuleFor(p => p.PlantationDate).LessThanOrEqualTo(DateTime.Today).WithMessage("Plantation date cannot be in the future.");
        RuleFor(p => p.SurfaceAreaRequired).GreaterThan(0).WithMessage("Surface area required must be greater than zero.");
        RuleFor(p => p.IdealHumidityLevel).InclusiveBetween(0, 100).WithMessage("Ideal humidity level must be between 0 and 100.");
    }
}