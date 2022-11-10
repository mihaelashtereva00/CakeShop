using CakeShop.DL.MongoRepositories;
using CakeShop.Models.MediatRCommands.CakeCommands;
using CakeShop.Models.Models;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CakeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CakeController : ControllerBase
    {
       // private readonly ICakeService _cakeService;
        private readonly ILogger<CakeController> _logger;
        private readonly IMediator _mediator;

        public CakeController(ILogger<CakeController> logger, IMediator mediator)
        {
           // _cakeService = cakeService;
            _logger = logger;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Add cake")]
        public async Task<IActionResult> AddCake(CakeRequest cakeRequest)
        {
            var result = await _mediator.Send(new AddCakeCommand(cakeRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result); 
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(nameof(UpdateCake))]
        public async Task<IActionResult> UpdateCake(UpdateCakeRequest cakeRequest)
        {
            var result = await _mediator.Send(new UpdateCakeCommand(cakeRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete(nameof(DeleteCake))]
        public async Task<IActionResult> DeleteCake(int cakeId)
        {
            var result = await _mediator.Send(new DeleteCakeCommand(cakeId));

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(GetAllCakes))]
        public async Task<IActionResult> GetAllCakes()
        {
            var result = await _mediator.Send(new GetAllCakesCommand());

            if (result.Cakes.Count()<1)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet(nameof(GetCakeById))]
        public async Task<IActionResult> GetCakeById(int cakeId)
        {
            var result = await _mediator.Send(new GetCakeByIdCommand(cakeId));

            if (result==null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
