using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.BakerCommands;
using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Responses.BakerResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.BakerHandlers
{
    public class GetByIdBakerHandler : IRequestHandler<GetBakereByIdCommand, BakerResponse>
    {
        public IBakerRepository _bakerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdBakerHandler> _logger;

        public GetByIdBakerHandler(IBakerRepository bakerRepository, IMapper mapper, ILogger<GetByIdBakerHandler> logger)
        {
            _bakerRepository = bakerRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<BakerResponse> Handle(GetBakereByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var baker = await _bakerRepository.GetBakertById(request.id);

                if (baker == null)
                {
                    _logger.LogError("A baker with that ID doesn't exist");
                    return new BakerResponse()
                    {
                        Baker = baker,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "The baker does not exist"
                    };
                }

                return new BakerResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Baker = baker,
                    Message = "Successfully got baker"
                };
            }
            catch (Exception)
            {
                _logger.LogError("Could not get baker by that Id");
            }

            return new BakerResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Baker = null,
                Message = "Could not get baker by that Id"
            };
        }
    }
}
