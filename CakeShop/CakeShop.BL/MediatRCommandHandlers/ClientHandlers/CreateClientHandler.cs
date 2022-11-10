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
    public class CreateClientHandler : IRequestHandler<CreateClientCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateClientHandler> _logger;

        public CreateClientHandler(IClientRepository clientRepository, IMapper mapper, ILogger<CreateClientHandler> logger)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ClientResponse> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _mapper.Map<Client>(request.clientRequest);
                await _clientRepository.CreateClient(result);

                return new ClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Client = result,
                    Message = "Successfully added client"
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Can not add client");
            }

            return new ClientResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Client = null,
                Message = "Can not add client"
            };
        }

    }
}
