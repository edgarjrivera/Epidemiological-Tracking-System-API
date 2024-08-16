using EpidemiologicalTrackingApi.Models;
using FluentValidation;

namespace EpidemiologicalTrackingApi.Validators
{
    public class IndividualValidator : AbstractValidator<Individual>
    {
        public IndividualValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID must be a positive integer.")
                .When(x => x.Id != 0);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.")
                .Matches("^[a-zA-Z ]*$").WithMessage("Name can only contain letters and spaces.");

            RuleFor(x => x.Age)
                .InclusiveBetween(0,120)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Age must be non-negative or must be between 0 and 120");

            RuleFor(x => x.Symptoms)
                .NotNull().WithMessage("Symptoms list cannot be null.")
                .NotEmpty().WithMessage("Symptoms list must contain at least one symptom.")
                .ForEach(symptom => symptom.NotEmpty().WithMessage("Each symptom must not be empty.")
                .Length(2, 50).WithMessage("Each symptom must be between 2 and 50 characters."));

            RuleFor(x => x.Diagnosed)
                .Must(x => x)
                .WithMessage("Diagnosed must be true.");

            RuleFor(x => x.DateDiagnosed)
                .NotNull()
                .NotEmpty()
                .GreaterThan(DateTime.MinValue).WithMessage("The date diagnosed must be a valid date and should not be 00/00/00.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("The date diagnosed cannot be in the future.");

        }
    }
}
