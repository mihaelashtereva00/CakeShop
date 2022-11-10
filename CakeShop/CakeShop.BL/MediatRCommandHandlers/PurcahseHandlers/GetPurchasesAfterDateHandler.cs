using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.PurchaseCommands;
using CakeShop.Models.Models.Responses.PurchaseResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.PurcahseHandlers
{
    public class GetPurchasesAfterDateHandler : IRequestHandler<GetPurchasesAfterDateCommand, GetAllPurchaseResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private ILogger<GetPurchasesAfterDateHandler> _logger;

        public GetPurchasesAfterDateHandler(IPurchaseRepository purchaseRepository, ILogger<GetPurchasesAfterDateHandler> logger)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
        }
        public async Task<GetAllPurchaseResponse> Handle(GetPurchasesAfterDateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var purchase = await _purchaseRepository.GetPurchasesAfterDate(request.date);

                if (purchase == null)
                {
                    _logger.LogError("Insert a valid purchase date");
                    return new GetAllPurchaseResponse()
                    {
                        Purchases = purchase,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "No purchases"
                    };
                }

                return new GetAllPurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Purchases = purchase,
                    Message = "Successfully got purchases"
                };
            }
            catch (Exception)
            {
                _logger.LogError("Gan not get purchases after that date");
            }

            return new GetAllPurchaseResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Purchases = null,
                Message = "Gan not get purchases after that date"
            };
        }
    }
}
