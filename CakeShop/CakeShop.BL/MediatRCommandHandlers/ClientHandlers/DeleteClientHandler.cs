using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.ClientCommands;
using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Responses.ClientResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.ClientHandlers
{
    public record DeleteClientHandler : IRequestHandler<DeleteClientCommand, ClientResponse>
    {
        private IClientRepository _clientRepository;
        private ILogger<DeleteClientHandler> _logger;
        private IMapper _mapper;

        public DeleteClientHandler(IClientRepository clientRepository, ILogger<DeleteClientHandler> logger, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ClientResponse> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientRepository.GetClient(request.username);

                if (client == null)
                {
                    _logger.LogError("Insert a valid client Id");
                }

                await _clientRepository.DeleteClient(request.username);
                _logger.LogInformation("The client is successfully deleted");

                return new ClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Client = client,
                    Message = "Successfully deleted client"
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Can not delete client");
            }

            return new ClientResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Client = null,
                Message = "Can not delete client"
            };
        }
    }
}
