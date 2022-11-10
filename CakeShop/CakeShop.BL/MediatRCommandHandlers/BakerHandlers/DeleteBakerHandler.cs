using AutoMapper;
using CakeShop.DL.Interfaces;
using CakeShop.Models.MediatRCommands.BakerCommands;
using CakeShop.Models.Models;
using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Responses.BakerResponses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.MediatRCommandHandlers.BakerHandlers
{
    public class DeleteBakerHandler : IRequestHandler<DeleteBakerCommand, BakerResponse>
    {
        public IBakerRepository _bakerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteBakerHandler> _logger;

        public DeleteBakerHandler(IBakerRepository bakerRepository, IMapper mapper, ILogger<DeleteBakerHandler> logger)
        {
            _bakerRepository = bakerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BakerResponse> Handle(DeleteBakerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bakerById = await _bakerRepository.GetBakertById(request.id);

                if (bakerById == null)
                {
                    _logger.LogError("Insert a valid baker Id");
                    return new BakerResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Baker = null,
                        Message = "Could not find baker with that Id"
                    };
                }

                var baker = await _bakerRepository.DeleteBaker(request.id);

                _logger.LogInformation("The baker successfully deleted");
                return new BakerResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Baker = baker,
                    Message = "Successfully deleted baker"
                };
            }
            catch (Exception)
            {
                _logger.LogError("Could not delete baker");
            }

            return new BakerResponse()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Baker = null,
                Message = "Could not delete baker"
            };
        }
    }
}
