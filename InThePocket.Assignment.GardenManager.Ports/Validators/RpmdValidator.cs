using FluentValidation;
using InThePocket.Assignment.GardenManager.Contracts.Api.Dto;

namespace InThePocket.Assignment.GardenManager.Ports.Validators;

public class RpmdValidator : AbstractValidator<RealtimePlantMetricDataDto>
{
    public RpmdValidator()
    {
        RuleFor(x => x.CurrentHumidityLevel).InclusiveBetween(0,100).WithMessage("CurrentHumidityLevel must be between 0 and 100.");
        RuleFor(x => x.LastIrrigationStartTime).LessThan(DateTime.Now).WithMessage("LastIrrigationStartTime must be in the past.");
        RuleFor(x => x.LastIrrigationEndTime)
            .LessThan(DateTime.Now)
            .GreaterThan(x => x.LastIrrigationStartTime)
            .WithMessage("LastIrrigationEndTime must be in the past and after LastIrrigationStartTime.");
    }
    
}