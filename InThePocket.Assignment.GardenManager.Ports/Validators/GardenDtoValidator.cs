using FluentValidation;
using InThePocket.Assignment.GardenManager.Contracts.Dto;

namespace InThePocket.Assignment.GardenManager.Ports.Validators;

public sealed class GardenDtoValidator : AbstractValidator<GardenDto>
{
    public GardenDtoValidator()
    {
        // GardenId is not validated as it is not always required (e.g. when creating a new garden)
        RuleFor(g => g.GardenName).NotEmpty().WithMessage("Garden name must not be empty.");
        RuleFor(g => g.TotalSurfaceArea).GreaterThan(0).WithMessage("Total surface area must be greater than zero.");
        RuleFor(g => g.LocationDescription).NotEmpty().WithMessage("Location description must not be empty.");
    }
}