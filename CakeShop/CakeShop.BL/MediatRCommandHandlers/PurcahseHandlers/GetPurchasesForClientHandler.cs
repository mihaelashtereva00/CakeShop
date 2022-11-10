using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.PurchaseCommands;
using CakeShop.Models.Models.Responses.PurchaseResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.PurcahseHandlers
{
    public class GetPurchasesForClientHandler : IRequestHandler<GetPurchasesForClientCommand, GetAllPurchaseResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private ILogger<GetPurchasesForClientHandler> _logger;

        public GetPurchasesForClientHandler(IPurchaseRepository purchaseRepository, ILogger<GetPurchasesForClientHandler> logger)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
        }

        public async Task<GetAllPurchaseResponse> Handle(GetPurchasesForClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var purchases = await _purchaseRepository.GetPurchasesForClient(request.id);

                if (purchases.Count() < 1)
                {
                    _logger.LogWarning("This client doesn't have purchases");
                }

                return new GetAllPurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Purchases = purchases,
                    Message = $"Successfully got all purchases for user with Id {request.id}"
                };
            }
            catch (Exception)
            {
                _logger.LogError("Can not get purchases for this client");
            }

            return new GetAllPurchaseResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Purchases = null,
                Message = "Can not get purchases for this client"
            };
        }
    }
}
