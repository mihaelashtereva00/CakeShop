using CakeShop.Models.Models.Responses.PurchaseResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.PurchaseCommands
{
    public class GetAllProcessedPurchasesCommand : IRequest<GetAllPurchaseResponse>
    {
    }
}
