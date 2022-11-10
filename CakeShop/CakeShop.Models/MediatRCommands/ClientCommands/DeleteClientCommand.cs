using CakeShop.Models.Models.Responses.ClientResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.ClientCommands
{
    public record DeleteClientCommand(string username):IRequest<ClientResponse>
    {
    }
}
