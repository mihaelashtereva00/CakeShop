using AutoMapper;
using CakeShop.BL.Interfaces;
using CakeShop.DL.Interfaces;
using CakeShop.Models.Models;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CakeShop.BL.Services
{
    public class BakerService : IBakerService
    {
        public IBakerRepository _bakerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BakerService> _logger;

        public BakerService(IBakerRepository bakerRepository, IMapper mapper, ILogger<BakerService> logger)
        {
            _bakerRepository = bakerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BakerResponse> AddBaker(BakerRequest bakerRequest)
        {
            try
            {
                var baker = await _bakerRepository.GetBakertByName(bakerRequest.Name);

                if (baker != null)
                    return new BakerResponse()
                    {
                        Baker = baker,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Baker with that name already exist"
                    };

                var bakerMapped = _mapper.Map<Baker>(bakerRequest);
                var result = await _bakerRepository.AddBaker(bakerMapped);

                return new BakerResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Baker =  result,
                    Message = "Successfully added baker"
                };

            }
            catch (Exception e)
            {
                _logger.LogError("Can not add baker");
                throw new Exception(e.Message);
            }
        }

        public async Task<Baker?> DeleteBaker(Guid bakerId)
        {
            var baker = await _bakerRepository.DeleteBaker(bakerId);

            if (baker == null)
            {
                _logger.LogError("Insert a valid baker Id");
            }

            _logger.LogInformation("The baker successfully deleted");
            return baker;
        }

        public async Task<IEnumerable<Baker>> GetAllBakers()
        {
            var bakers = await _bakerRepository.GetAllBakers();

            if (bakers.Count() < 1)
            {
                _logger.LogWarning("There are no bakers added");
            }

            return bakers;
        }

        public Task<Baker?> GetBakertById(Guid id)
        {
            var baker = _bakerRepository.GetBakertById(id);

            if (baker == null)
            {
                _logger.LogError("A baker with that ID doesn't exist");
            }

            return baker;
        }

        public async Task<BakerResponse> UpdateBaker(Baker baker)
        {
            try
            {
                var bakerUpdate = await _bakerRepository.GetBakertById(baker.Id);

                if (bakerUpdate == null)
                {
                    return new BakerResponse()
                    {
                        Baker = baker,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "The baker does not exist"
                    };
                }
                
                var result = await _bakerRepository.UpdateBaker(baker);

                return new BakerResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Baker = result,
                    Message = "Successfully updated baker"
                };

            }
            catch (Exception e)
            {
                _logger.LogError($"Error when updating baker with Id {baker.Id} : {e}");
                throw new Exception(e.Message); ;
            }
        }
    }
}
