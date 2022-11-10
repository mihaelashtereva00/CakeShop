using CakeShop.Models.Models.Requests;
using FluentValidation;

namespace CakeShop.Validators.PurchaseValidator
{
    public class AddPurchaseValidator : AbstractValidator<PurchaseRequest>
    {
        public AddPurchaseValidator()
        {
            RuleFor(p => p.Cakes)
                .Must(c => c.Count() > 0)
                .WithMessage("There must be at least 1 cake in the purchase");

            RuleFor(p => p.ClientId)
                .GreaterThan(0);

        }
    }
}
