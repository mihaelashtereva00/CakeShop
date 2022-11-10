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
    public class UpdatePurcahseHandler : IRequestHandler<UpdatePurchaseCommand, PurchaseResponse>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICakeRepository _cakeRepository;
        private ILogger<UpdatePurcahseHandler> _logger;
        private IMapper _mapper;

        public UpdatePurcahseHandler(IPurchaseRepository purchaseRepository, ILogger<UpdatePurcahseHandler> logger, IMapper mapper, IClientRepository clientRepository, ICakeRepository cakeRepository)
        {
            _purchaseRepository = purchaseRepository;
            _logger = logger;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _cakeRepository = cakeRepository;
        }

        public async Task<PurchaseResponse> Handle(UpdatePurchaseCommand purchaseRequest, CancellationToken cancellationToken)
        {
            try
            {
                var prc = new Purchase();
                decimal totalMoney = 0;
                List<Cake> cakeList = new List<Cake>();

                foreach (var id in purchaseRequest.updatePurchaseRequest.Cakes)
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

                prc.Id = purchaseRequest.updatePurchaseRequest.Id;
                prc.Cakes = cakeList;
                prc.Date = DateTime.Now;
                prc.TotalMoney = totalMoney;
                prc.ClientId = _purchaseRepository.GetPurchasesById(prc.Id).Result.ClientId;

                await _purchaseRepository.UpdatePurchase(prc);

                return new PurchaseResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Purchase = prc,
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
