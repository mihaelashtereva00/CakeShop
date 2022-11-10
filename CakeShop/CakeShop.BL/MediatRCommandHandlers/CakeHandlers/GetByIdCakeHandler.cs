using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.BakerCommands;
using CakeShop.Models.MediatRCommands.CakeCommands;
using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Responses.CakeResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.CakeHandlers
{
    public class GetByIdCakeHandler : IRequestHandler<GetCakeByIdCommand, CakeResponse>
    {
        public ICakeRepository _cakeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdCakeHandler> _logger;

        public GetByIdCakeHandler(ICakeRepository cakeRepository, IMapper mapper, ILogger<GetByIdCakeHandler> logger)
        {
            _cakeRepository = cakeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CakeResponse> Handle(GetCakeByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cake = await _cakeRepository.GetCakeById(request.id);

                if (cake == null)
                {
                    _logger.LogError("A cake with that ID doesn't exist");
                    return new CakeResponse()
                    {
                        Cake = cake,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "The cake does not exist"
                    };
                }

                return new CakeResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Cake = cake,
                    Message = "Successfully got cake"
                };
            }
            catch (Exception)
            {
                _logger.LogError("Could not get cake");
            }

            return new CakeResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Cake = null,
                Message = "Could not get cake"
            };
        }
    }
}
