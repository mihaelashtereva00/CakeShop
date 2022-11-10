using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.PurchaseCommands;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses.PurchaseResponses;
using CakeShop.Models.ModelsMongoDB;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.BL.MediatRCommandHandlers.PurcahseHandlers
{
    public class UpdatePurcahseHandler : IRequestHandler<UpdatePurchaseCommand, PurchaseResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IClientRepository _clientRepository;
        private ILogger<UpdatePurcahseHandler> _logger;
        private IMapper _mapper;

        public UpdatePurcahseHandler(IPurchaseRepository purchaseRepository, ILogger<UpdatePurcahseHandler> logger, IMapper mapper, IClientRepository clientRepository)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task<PurchaseResponse> Handle(UpdatePurchaseCommand purchaseRequest, CancellationToken cancellationToken)
        {
            try
            {
                var client = _clientRepository.GetClientById(purchaseRequest.updatePurchaseRequest.ClientId).Result;
                decimal totalMoney = 0;
                var result = _mapper.Map<Purchase>(purchaseRequest.updatePurchaseRequest);

                if (client == null)
                {
                    return new PurchaseResponse()
                    {
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Purchase = result,
                        Message = $"Cant find client with that id: {purchaseRequest.updatePurchaseRequest.ClientId}"
                    };
                }

                foreach (var cake in purchaseRequest.updatePurchaseRequest.Cakes)
                {
                    totalMoney += cake.Price;
                }

                result.TotalMoney = totalMoney;
                await _purchaseRepository.UpdatePurchase(result);

                return new PurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Purchase = result,
                    Message = "Successfully updated purchase"
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Can not update purchase");
            }

            return new PurchaseResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Purchase = null,
                Message = "Can not update purchase"
            };
        }
    }
}
