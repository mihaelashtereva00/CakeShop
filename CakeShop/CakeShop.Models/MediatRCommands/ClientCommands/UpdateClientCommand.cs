using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses.ClientResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.ClientCommands
{
    public record UpdateClientCommand(UpdateClientRequest updateClientRequest) : IRequest<ClientResponse>
    {
    }
}
