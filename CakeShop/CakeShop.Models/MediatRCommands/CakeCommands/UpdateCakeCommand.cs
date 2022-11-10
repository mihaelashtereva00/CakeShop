using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses.CakeResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.CakeCommands
{
    public record UpdateCakeCommand(UpdateCakeRequest cakeRequest) : IRequest<CakeResponse>
    {
    }
}
