using CakeShop.Models.Models.Responses.CakeResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.CakeCommands
{
    public record GetAllCakesCommand(): IRequest<GetAllCakesResponse>
    {
    }
}
