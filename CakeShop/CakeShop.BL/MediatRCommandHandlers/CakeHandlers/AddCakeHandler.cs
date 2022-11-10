using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.DL.MongoRepositories;
using CakeShop.DL.SqlRepositories;
using CakeShop.Models.MediatRCommands.BakerCommands;
using CakeShop.Models.MediatRCommands.CakeCommands;
using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses.BakerResponses;
using CakeShop.Models.Models.Responses.CakeResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.CakeHandlers
{
    public record AddCakeHandler : IRequestHandler<AddCakeCommand, CakeResponse>
    {
        public ICakeRepository _cakeRepository;
        public IBakerRepository _bakerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddCakeHandler> _logger;

        public AddCakeHandler(ICakeRepository cakeRepository, IMapper mapper, ILogger<AddCakeHandler> logger, IBakerRepository bakerRepository)
        {
            _cakeRepository = cakeRepository;
            _mapper = mapper;
            _logger = logger;
            _bakerRepository = bakerRepository;
        }
        public async Task<CakeResponse> Handle(AddCakeCommand request, CancellationToken cancellationToken)
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
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Cake = null,
                        Message = "Could not add cake"
                    };
                }

                await _cakeRepository.AddCake(cake);

                return new CakeResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Cake = cake,
                    Message = "Successfully added cake"
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Can not add cake");
            }

            return new CakeResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Cake = null,
                Message = "Can not add cake"
            };
        }
    }
}
