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
    public class GetClientHandler : IRequestHandler<GetClientCommand, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetClientCommand> _logger;

        public GetClientHandler(IMapper mapper, ILogger<GetClientCommand> logger, IClientRepository clientRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _clientRepository = clientRepository;
        }

        public async Task<ClientResponse> Handle(GetClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientRepository.GetClient(request.username);

                if (client == null)
                {
                    return new ClientResponse()
                    {
                        HttpStatusCode = HttpStatusCode.OK,
                        Client = client,
                        Message = "Could not find client"
                    };
                    _logger.LogError("Insert a valid client Id");
                }

                return new ClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Client = client,
                    Message = "Successfully found client"
                };

            }
            catch (Exception e)
            {
                _logger.LogError("Can not find client");
            }

            return new ClientResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Client = null,
                Message = "Could not find client"
            };
        }
    }
}
