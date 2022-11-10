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
    public class DeleteCakeHandler : IRequestHandler<DeleteCakeCommand, CakeResponse>
    {
        public ICakeRepository _cakeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCakeHandler> _logger;

        public DeleteCakeHandler(ICakeRepository cakeRepository, IMapper mapper, ILogger<DeleteCakeHandler> logger)
        {
            _cakeRepository = cakeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CakeResponse> Handle(DeleteCakeCommand request, CancellationToken cancellationToken)
        {
            try
            {
            var cake = _cakeRepository.GetCakeById(request.id).Result;

            if (cake == null)
            {
                _logger.LogError("Insert a valid cake Id");

                return new CakeResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Cake = cake,
                    Message = "Could not find cake with that Id"
                };
            }

            _cakeRepository.DeleteCake(request.id);
            _logger.LogInformation("The cake is successfully deleted");

            return new CakeResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Cake = cake,
                Message = "Successfully deleted cake"
            };
            }
            catch (Exception)
            {
                _logger.LogError("Could not delete cake");
            }

            return new CakeResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Cake = null,
                Message = "Could not delete cake"
            };
        }
    }
}
