using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.CakeCommands;
using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Responses.CakeResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.CakeHandlers
{
    public class UpdateCakeHandelr : IRequestHandler<UpdateCakeCommand, CakeResponse>
    {
        public ICakeRepository _cakeRepository;
        public IBakerRepository _bakerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCakeHandelr> _logger;

        public UpdateCakeHandelr(ICakeRepository cakeRepository, IMapper mapper, ILogger<UpdateCakeHandelr> logger, IBakerRepository bakerRepository)
        {
            _cakeRepository = cakeRepository;
            _mapper = mapper;
            _logger = logger;
            _bakerRepository = bakerRepository;
        }

        public async Task<CakeResponse> Handle(UpdateCakeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var baker = await _bakerRepository.GetBakertById(request.cakeRequest.BakerId);
                if (baker == null)
                {
                    return new CakeResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Cake = null,
                        Message = "This baker Id is not valid"
                    };
                }

                var cake = _mapper.Map<Cake>(request.cakeRequest);
                if (cake == null)
                {
                    return new CakeResponse()
                    {
                        Cake = cake,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "The cake does not exist"
                    };
                }

                var result = await _cakeRepository.UpdateCake(cake);

                return new CakeResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Cake = result,
                    Message = "Successfully updated cake"
                };

            }
            catch (Exception e)
            {
                _logger.LogError($"Error when updating cake with Id {request.cakeRequest.Id} : {e}");
            }

            return new CakeResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Cake = null,
                Message = "Could not update cake"
            };
        }
    }
}
