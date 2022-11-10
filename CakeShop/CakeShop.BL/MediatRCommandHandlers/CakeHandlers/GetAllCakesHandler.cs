using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.CakeCommands;
using CakeShop.Models.Models.Responses.CakeResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.CakeHandlers
{
    public class GetAllCakesHandler : IRequestHandler<GetAllCakesCommand, GetAllCakesResponse>
    {
        public ICakeRepository _cakeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCakesHandler> _logger;

        public GetAllCakesHandler(ICakeRepository cakeRepository, IMapper mapper, ILogger<GetAllCakesHandler> logger)
        {
            _cakeRepository = cakeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetAllCakesResponse> Handle(GetAllCakesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cakes = await _cakeRepository.GetAllCakes();
                if (cakes.Count() < 1)
                {
                    _logger.LogWarning("There are no cakes added");
                }

                return new GetAllCakesResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Cakes = cakes,
                    Message = "Successfully got all cakes"
                };
            }
            catch (Exception)
            {
                _logger.LogError("Could not get all cakes");
            }


            return new GetAllCakesResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Cakes = null,
                Message = "Could not get all cakes"
            };
        }
    }
}
