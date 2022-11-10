using CakeShop.Models.Models.Requests;
using FluentValidation;

namespace CakeShop.Validators.PurchaseValidator
{
    public class UpdatePurchaseValidator : AbstractValidator<UpdatePurchseCakesRequest>
    {
        public UpdatePurchaseValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .NotEmpty();

            RuleFor(p => p.Cakes)
                .Must(c => c.Count() > 0);

            //RuleFor(p => p.ClientId)
            //    .GreaterThan(0);
        }
    }
}
