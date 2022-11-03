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
    public class CakeService : ICakeService
    {
        public ICakeRepository _cakeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CakeService> _logger;

        public CakeService(ICakeRepository cakeRepository, IMapper mapper, ILogger<CakeService> logger)
        {
            _cakeRepository = cakeRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CakeResponse> AddCake(CakeRequest cakeRequest)
        {
            try
            {
                var bakerMapped = _mapper.Map<Cake>(cakeRequest);
                var result = await _cakeRepository.AddCake(cakeRequest);

                return new CakeResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Cake =  result,
                    Message = "Successfully added cake"
                };

            }
            catch (Exception e)
            {
                _logger.LogError("Can not add baker");
                throw new Exception(e.Message);
            }
        }

        public async Task<Cake?> DeleteCake(Guid cakeId)
        {
            var cake = await _cakeRepository.GetCakeById(cakeId);

            if (cake == null)
            {
                _logger.LogError("Insert a valid cake Id");
            }

            _cakeRepository.DeleteCake(cakeId);
            _logger.LogInformation("The cake is successfully deleted");
            return cake;
        }

        public async Task<IEnumerable<Cake>> GetAllCakes()
        {
            var cakes = await _cakeRepository.GetAllCakes();

            if (cakes.Count() < 1)
            {
                _logger.LogWarning("There are no bakers added");
            }

            return cakes;
        }

        public Task<Cake?> GetCakeById(Guid id)
        {
            var baker = _cakeRepository.GetCakeById(id);

            if (baker == null)
            {
                _logger.LogError("A cake with that ID doesn't exist");
            }

            return baker;
        }

        public async Task<CakeResponse> UpdateCake(Cake cake)
        {
            try
            {
                var cakeUpdate = await _cakeRepository.UpdateCake(cake);

                if (cakeUpdate == null)
                {
                    return new CakeResponse()
                    {
                        Cake = cakeUpdate,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "The cake does not exist"
                    };
                }

                var result = await _cakeRepository.UpdateCake(cakeUpdate);

                return new CakeResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Cake = result,
                    Message = "Successfully updated cake"
                };

            }
            catch (Exception e)
            {
                _logger.LogError($"Error when updating cake with Id {cake.Id} : {e}");
                throw new Exception(e.Message); ;
            }
        }
    }
}
