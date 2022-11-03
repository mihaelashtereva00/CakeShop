using AutoMapper;
using CakeShop.BL.Interfaces;
using CakeShop.DL.Interfaces;
using CakeShop.Models.Models;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses;
using CakeShop.Models.ModelsMongoDB;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private ILogger<PurchaseService> _logger;
        private IMapper _mapper;

        public PurchaseService(IPurchaseRepository purchaseRepository, ILogger<PurchaseService> logger, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PurchaseResponse?> CreatePurchase(PurchaseRequest purchaseRequest)
        {
            try
            {
                var purcahse = await _purchaseRepository.CreatePurchase(purchaseRequest);

                var result = _mapper.Map<Purchase>(purchaseRequest);

                return new PurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Purchase = result,
                    Message = "Successfully added purchase"
                };

            }
            catch (Exception e)
            {
                _logger.LogError("Can not add purchase");
                throw new Exception(e.Message);
            }
        }

        public async Task<Purchase> DeletePurchase(Guid purchaseId)
        {
            var purchase = await _purchaseRepository.DeletePurchase(purchaseId);

            if (purchase == null)
            {
                _logger.LogError("Insert a valid baker Id");
            }

            _logger.LogInformation("The baker successfully deleted");
            return purchase;
        }

        public Task<Purchase> GetPurchases(Guid clientId)
        {
            throw new NotImplementedException();
        }
    }
}
