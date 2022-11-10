using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.PurchaseCommands;
using CakeShop.Models.Models.Responses.PurchaseResponses;
using CakeShop.Models.ModelsMongoDB;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.PurcahseHandlers
{
    public class GetPurchaseByIdHandler : IRequestHandler<GetPurcahseCommand, PurchaseResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private ILogger<GetPurchaseByIdHandler> _logger;

        public GetPurchaseByIdHandler(IPurchaseRepository purchaseRepository, ILogger<GetPurchaseByIdHandler> logger)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
        }
        public async Task<PurchaseResponse> Handle(GetPurcahseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var purchase = await _purchaseRepository.GetPurchasesById(request.id);

                if (purchase == null)
                {
                    _logger.LogError("Insert a valid purchase Id");
                    return new PurchaseResponse()
                    {
                        Purchase = purchase,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "The purchase with that ID does not exist"
                    };
                }

                return new PurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Purchase = purchase,
                    Message = "Successfully got purchase"
                };
            }
            catch (Exception)
            {
                _logger.LogError("Can not get purchase");
            }

            return new PurchaseResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Purchase = null,
                Message = "Can not get purchase"
            };
        }
    }
}
