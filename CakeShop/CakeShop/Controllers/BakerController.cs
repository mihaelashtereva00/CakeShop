using CakeShop.Models.MediatRCommands.BakerCommands;
using CakeShop.Models.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CakeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BakerController : ControllerBase
    {
        private readonly ILogger<BakerController> _logger;
        private readonly IMediator _mediator;

        public BakerController( ILogger<BakerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Add baker")]
        public async Task<IActionResult> AddBaker([FromBody] BakerRequest bakerRequest)
        {
            var result = await _mediator.Send(new AddBakerCommand(bakerRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateBaker))]
        public async Task<IActionResult> UpdateBaker(UpdateBakerRequest bakerRequest)
        {
            var result = await _mediator.Send(new UpdateBakerCommand(bakerRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteBaker))]
        public async Task<IActionResult> DeleteBaker(int bakerId)
        {
            var result = await _mediator.Send(new DeleteBakerCommand(bakerId));

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(GetAllBakers))]
        public async Task<IActionResult> GetAllBakers()
        {
            var result = await _mediator.Send(new GetAllBakersCommand());

            if (result.Bakers.Count() < 1)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(GetBakerById))]
        public async Task<IActionResult> GetBakerById(int bakerId)
        {
            var result = await _mediator.Send(new GetBakereByIdCommand(bakerId));

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
