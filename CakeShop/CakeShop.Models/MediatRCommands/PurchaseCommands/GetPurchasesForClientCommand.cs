using CakeShop.Models.Models.Responses.PurchaseResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.PurchaseCommands
{
    public record GetPurchasesForClientCommand(int id) : IRequest<GetAllPurchaseResponse>
    {
    }
}
