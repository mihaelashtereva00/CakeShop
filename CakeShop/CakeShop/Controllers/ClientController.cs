using CakeShop.Models.MediatRCommands.ClientCommands;
using CakeShop.Models.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CakeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Add client")]
        public async Task<IActionResult> AddClient([FromBody] ClientRequest clientRequest)
        {
            var result = await _mediator.Send(new CreateClientCommand(clientRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("Update client")]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientRequest clientRequest)
        {
            var result = await _mediator.Send(new UpdateClientCommand(clientRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Get client by username")]
        public async Task<IActionResult> GetClient(string username)
        {
            var result = await _mediator.Send(new GetClientCommand(username));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("Delete client by username")]
        public async Task<IActionResult> DeleteClient(string username)
        {
            var result = await _mediator.Send(new DeleteClientCommand(username));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
