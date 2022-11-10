using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.PurchaseCommands;
using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Responses.PurchaseResponses;
using CakeShop.Models.ModelsMongoDB;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.PurcahseHandlers
{
    public class CreatePurcahseHandler : IRequestHandler<CreatePurcahseCommand, PurchaseResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICakeRepository _cakeRepository;
        private ILogger<CreatePurcahseHandler> _logger;
        private IMapper _mapper;

        public CreatePurcahseHandler(IPurchaseRepository purchaseRepository, ILogger<CreatePurcahseHandler> logger, IMapper mapper, IClientRepository clientRepository, ICakeRepository cakeRepository)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _cakeRepository = cakeRepository;
        }

        public async Task<PurchaseResponse> Handle(CreatePurcahseCommand purchaseRequest, CancellationToken cancellationToken)
        {
            try
            {
                var client = _clientRepository.GetClientById(purchaseRequest.purchaseRequest.ClientId).Result;
                if (client == null)
                {
                    return new PurchaseResponse()
                    {
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Purchase = null,
                        Message = $"Cant find client with that id: {purchaseRequest.purchaseRequest.ClientId}"
                    };
                }

                var prc = new Purchase();
                decimal totalMoney = 0;
                List<Cake> cakeList = new List<Cake>();

                foreach (var id in purchaseRequest.purchaseRequest.Cakes)
                {
                    var cake = await _cakeRepository.GetCakeById(id);
                    if (cake != null)
                    {
                        cakeList.Add(cake);
                        totalMoney += cake.Price;
                    }
                }

                if (cakeList.Count() == 0)
                {
                    return new PurchaseResponse()
                    {
                        HttpStatusCode = HttpStatusCode.NotFound,
                        Purchase = null,
                        Message = $"Please enter at least 1 valid cake Id"
                    };
                }

                prc.Cakes = cakeList;
                prc.Id = Guid.NewGuid();
                prc.Date = DateTime.Now;
                prc.TotalMoney = totalMoney;
                prc.ClientId = purchaseRequest.purchaseRequest.ClientId;

                await _purchaseRepository.CreatePurchase(prc);

                return new PurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Purchase = prc,
                    Message = "Successfully added purchase"
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Can not add purchase");
            }

            return new PurchaseResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Purchase = null,
                Message = "Can not add purchase"
            };
        }
    }
}
