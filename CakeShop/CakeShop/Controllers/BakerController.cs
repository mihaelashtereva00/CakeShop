using CakeShop.BL.Interfaces;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CakeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BakerController : ControllerBase
    {
        private readonly IBakerService _bakerService;
        private readonly ILogger<BakerController> _logger;
        //  private readonly IMediator _mediator;

        public BakerController(IBakerService bakerService, ILogger<BakerController> logger)//, IMediator mediator)
        {
            _bakerService = bakerService;
            _logger = logger;
            // _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Add baker")]
        public async Task<IActionResult> AddBaker(BakerRequest bakerRequest)
        {
            var result = await _bakerService.AddBaker(bakerRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateBaker))]
        public async Task<IActionResult> UpdateBaker(Baker baker)
        {
            var result = await _bakerService.UpdateBaker(baker);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteBaker))]
        public async Task<IActionResult> DeleteBaker(Guid bakerId)
        {
            var result = await _bakerService.DeleteBaker(bakerId);

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(GetAllBakers))]
        public async Task<IActionResult> GetAllBakers()
        {
            var result = await _bakerService.GetAllBakers();

            if (result.Count() < 1)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(GetBakerById))]
        public async Task<IActionResult> GetBakerById(Guid bakerId)
        {
            var result = await _bakerService.GetBakertById(bakerId);

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
