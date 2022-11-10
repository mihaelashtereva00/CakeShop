using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses.PurchaseResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.PurchaseCommands
{
    public record CreatePurcahseCommand(PurchaseRequest purchaseRequest) : IRequest<PurchaseResponse>
    {
    }
}
