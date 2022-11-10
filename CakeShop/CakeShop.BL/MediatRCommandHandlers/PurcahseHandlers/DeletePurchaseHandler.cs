using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.PurchaseCommands;
using CakeShop.Models.Models.Responses.PurchaseResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.PurcahseHandlers
{
    public class DeletePurchaseHandler : IRequestHandler<DeletePurchaseCommand, PurchaseResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private ILogger<DeletePurchaseHandler> _logger;

        public DeletePurchaseHandler(IPurchaseRepository purchaseRepository, ILogger<DeletePurchaseHandler> logger)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
        }

        public async Task<PurchaseResponse> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var purchase = await _purchaseRepository.DeletePurchase(request.id);

                if (purchase == null)
                {
                    _logger.LogError("Insert a valid purchase Id");
                    return new PurchaseResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Purchase = purchase,
                        Message = "Could not find purchase with that Id"
                    };
                }

                _logger.LogInformation("The purchase is successfully deleted");
                return new PurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Purchase = purchase,
                    Message = "Successfully deleted purchase"
                };
            }
            catch (Exception)
            {
                _logger.LogError("Could not delete");
            }

            return new PurchaseResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Purchase = null,
                Message = "Could not delete"
            };
        }
    }
}
