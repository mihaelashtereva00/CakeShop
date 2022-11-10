using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.BakerCommands;
using CakeShop.Models.Models.Responses.BakerResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.BakerHandlers
{
    public class GetAllBakersHandler : IRequestHandler<GetAllBakersCommand, GetAllBakersResponse>
    {
        private readonly IBakerRepository _bakerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllBakersHandler> _logger;

        public GetAllBakersHandler(IBakerRepository bakerRepository, IMapper mapper, ILogger<GetAllBakersHandler> logger)
        {
            _bakerRepository = bakerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetAllBakersResponse> Handle(GetAllBakersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bakers = await _bakerRepository.GetAllBakers();

                if (bakers.Count() < 1)
                {
                    _logger.LogWarning("There are no bakers added");
                    return new GetAllBakersResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Bakers = bakers,
                        Message = "There are no bakers added"
                    };
                }

                return new GetAllBakersResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Bakers = bakers,
                    Message = "Successfully got all bakers"
                };
            }
            catch (Exception)
            {
                _logger.LogError("Could not get bakers");
            }

            return new GetAllBakersResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Bakers = null,
                Message = "Could not get bakers"
            };
        }
    }
}
