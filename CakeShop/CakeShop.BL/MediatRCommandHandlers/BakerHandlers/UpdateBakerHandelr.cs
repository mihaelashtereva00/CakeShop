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
    public class UpdateBakerHandler : IRequestHandler<UpdateBakerCommand, BakerResponse>
    {
        public IBakerRepository _bakerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBakerHandler> _logger;

        public UpdateBakerHandler(IBakerRepository bakerRepository, IMapper mapper, ILogger<UpdateBakerHandler> logger)
        {
            _bakerRepository = bakerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BakerResponse> Handle(UpdateBakerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bakerById = await _bakerRepository.GetBakertById(request.updateBakerRequest.Id);
                if (bakerById == null)
                {
                    return new BakerResponse()
                    {
                        Baker = null,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "The baker does not exist"
                    };
                }

                var baker = _mapper.Map<Baker>(request.updateBakerRequest);

                await _bakerRepository.UpdateBaker(baker);

                return new BakerResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Baker = baker,
                    Message = "Successfully updated baker"
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Error when updating baker with Id {request.updateBakerRequest.Id} : {e}");
            }

            return new BakerResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Baker = null,
                Message = "Could not update baker"
            };
        }
    }
}
