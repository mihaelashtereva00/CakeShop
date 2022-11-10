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
    public record UpdateClientHandler : IRequestHandler<UpdateClientCommand, ClientResponse>
    {
        private IClientRepository _clientRepository;
        private ILogger<UpdateClientHandler> _logger;
        private IMapper _mapper;

        public UpdateClientHandler(IClientRepository clientRepository, ILogger<UpdateClientHandler> logger, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ClientResponse> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _mapper.Map<Client>(request.updateClientRequest);

                await _clientRepository.Update(result);

                return new ClientResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Client = result,
                    Message = "Successfully updated client"
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Can not update client");
            }

            return new ClientResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Client = null,
                Message = "Can not update client"
            };
        }
    }
}
