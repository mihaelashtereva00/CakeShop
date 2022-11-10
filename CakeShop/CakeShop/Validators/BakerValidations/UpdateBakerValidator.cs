using CakeShop.Models.Models.Requests;
using FluentValidation;

namespace CakeShop.Validators.BakerValidations
{
    public class UpdateBakerValidator : AbstractValidator<UpdateBakerRequest>
    {
        public UpdateBakerValidator()
        {
            RuleFor(b => b.Id)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(b => b.Age)
                .NotNull()
                .NotEmpty()
                .GreaterThan(18)
                .LessThan(75)
                .WithMessage("The baker should be at least 18 years old!");

            RuleFor(b => b.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Enter a valid name");

            RuleFor(b => b.Specialty)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(50)
                .WithMessage("The specialty should be between 2 and 50 characters");

            RuleFor(b => b.DateOfBirth)
                .NotNull()
                .NotEmpty()
                .Must(d => d.Year > 2005)
                .WithMessage("The year should be before 2005");
        }
    }
}
