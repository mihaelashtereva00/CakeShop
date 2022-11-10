using CakeShop.Models.Models.Responses.BakerResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.BakerCommands
{
    public record GetBakereByIdCommand(int id) : IRequest<BakerResponse>
    {
    }
}
