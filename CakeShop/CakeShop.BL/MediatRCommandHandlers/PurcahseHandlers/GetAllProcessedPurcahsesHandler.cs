using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.PurchaseCommands;
using CakeShop.Models.Models.Responses.PurchaseResponses;
using MediatR;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.PurcahseHandlers
{
    public class GetAllProcessedPurcahsesHandler : IRequestHandler<GetAllProcessedPurchasesCommand, GetAllPurchaseResponse>
    {
        public IProcessedPurchasesRepository _processedPurchases;

        public GetAllProcessedPurcahsesHandler(IProcessedPurchasesRepository processedPurchases)
        {
            _processedPurchases = processedPurchases;
        }

        public async Task<GetAllPurchaseResponse> Handle(GetAllProcessedPurchasesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _processedPurchases.GetAllProcessedPurchases();
                return new GetAllPurchaseResponse()
                {
                    Purchases = result,
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = "Successfullt got all processed purchases"
                };
            }
            catch (Exception)
            {
                return new GetAllPurchaseResponse()
                {
                    Purchases = null,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Could not get all processed purchases"
                };
            }
        }
    }
}
