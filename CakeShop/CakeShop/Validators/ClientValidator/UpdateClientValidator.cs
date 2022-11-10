using CakeShop.Models.Models.Requests;
using FluentValidation;

namespace CakeShop.Validators.ClientValidator
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientRequest>
    {
        public UpdateClientValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(c => c.DateOfBirth)
                .GreaterThan(DateTime.MinValue)
                .LessThan(DateTime.MaxValue)
                .Must(d => d.Year > 2005)
                .WithMessage("The year should be before 2005");

            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(c => c.Username)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(c => c.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}
