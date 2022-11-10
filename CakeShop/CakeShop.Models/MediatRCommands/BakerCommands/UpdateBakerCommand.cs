using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses.BakerResponses;
using MediatR;

namespace CakeShop.Models.MediatRCommands.BakerCommands
{
    public record UpdateBakerCommand(UpdateBakerRequest updateBakerRequest) : IRequest<BakerResponse>
    {
    }
}
