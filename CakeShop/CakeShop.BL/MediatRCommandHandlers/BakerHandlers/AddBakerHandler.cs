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
    public record AddBakerHandler : IRequestHandler<AddBakerCommand, BakerResponse>
    {
        public IBakerRepository _bakerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddBakerHandler> _logger;

        public AddBakerHandler(IBakerRepository bakerRepository, IMapper mapper, ILogger<AddBakerHandler> logger)
        {
            _bakerRepository = bakerRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<BakerResponse> Handle(AddBakerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var baker = await _bakerRepository.GetBakertByName(request.bakerRequest.Name);

                if (baker != null)
                    return new BakerResponse()
                    {
                        Baker = baker,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Baker with that name already exist"
                    };

                var bakerMapped = _mapper.Map<Baker>(request.bakerRequest);
                var result = await _bakerRepository.AddBaker(bakerMapped);

                return new BakerResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Baker = result,
                    Message = "Successfully added baker"
                };

            }
            catch (Exception e)
            {
                _logger.LogError("Can not add baker");
            }

            return new BakerResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Baker = null,
                Message = "Could not add baker"
            };
        }
    }
}
