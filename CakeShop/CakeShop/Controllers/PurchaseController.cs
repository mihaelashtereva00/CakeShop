using CakeShop.Models.MediatRCommands.PurchaseCommands;
using CakeShop.Models.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CakeShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly IMediator _mediator;

        public PurchaseController(ILogger<PurchaseController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddPurcahse")]
        public async Task<IActionResult> AddPurcahse(PurchaseRequest purchaseRequest)
        {
            var result = await _mediator.Send(new CreatePurcahseCommand(purchaseRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetPurchasesForClient")]
        public async Task<IActionResult> GetPurchasesForClient(int clientId)
        {
            var result = await _mediator.Send(new GetPurchasesForClientCommand(clientId));

            if (result.Purchases.Count() < 1)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetPurchaseById")]
        public async Task<IActionResult> GetPurchaseById(Guid purchaseId)
        {
            var result = await _mediator.Send(new GetPurcahseCommand(purchaseId));

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("DeletePurchase")]
        public async Task<IActionResult> DeletePurchase(Guid purchaseId)
        {
            var result = await _mediator.Send(new DeletePurchaseCommand(purchaseId));

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("UpdatePurchase")]
        public async Task<IActionResult> UpdatePurchase(UpdatePurchseCakesRequest purchaseRequest)
        {
            var result = await _mediator.Send(new UpdatePurchaseCommand(purchaseRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Get all processed purchases")]
        public async Task<IActionResult> GetAllProcessedPurchases()
        {
            var result = await _mediator.Send(new GetAllProcessedPurchasesCommand());

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
