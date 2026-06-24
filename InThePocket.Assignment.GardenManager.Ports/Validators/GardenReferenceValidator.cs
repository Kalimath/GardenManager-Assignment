using FluentValidation;
using InThePocket.Assignment.GardenManager.Contracts.Api;

namespace InThePocket.Assignment.GardenManager.Ports.Validators;

public class GardenReferenceValidator : AbstractValidator<GardenReference>
{
    public GardenReferenceValidator()
    {
        RuleFor(x => x.GardenId).NotEmpty().WithMessage("GardenId must not be empty.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be empty.");
    }
}