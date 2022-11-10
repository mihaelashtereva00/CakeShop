using CakeShop.Models.Models.Responses.CakeResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.CakeCommands
{
    public record DeleteCakeCommand(int id): IRequest<CakeResponse>
    {
    }
}
