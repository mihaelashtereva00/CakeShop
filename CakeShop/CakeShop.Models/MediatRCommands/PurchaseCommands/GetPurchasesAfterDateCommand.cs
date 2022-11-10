using CakeShop.Models.Models.Responses.PurchaseResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.PurchaseCommands
{
    public record GetPurchasesAfterDateCommand(DateTime date) : IRequest<GetAllPurchaseResponse>
    {
    }
}
