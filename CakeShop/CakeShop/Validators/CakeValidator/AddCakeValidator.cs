using CakeShop.Models.Models.Requests;
using FluentValidation;

namespace CakeShop.Validators.CakeValidator
{
    public class AddCakeValidator : AbstractValidator<CakeRequest>
    {
        public AddCakeValidator()
        {
            RuleFor(c => c.Form)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(15);

            RuleFor(c => c.Price)
                .GreaterThan(12)
                .WithMessage("The cake must be at least 12$");

            RuleFor(c => c.BakerId)
                .GreaterThan(0);

            RuleFor(c => c.Topping)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(15);

            RuleFor(c => c.Base)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(15);
        }
    }
}
