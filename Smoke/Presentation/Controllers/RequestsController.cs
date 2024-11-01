using Application.Features.Requests.HttpRequest.Commands.CreateHttpRequest;
using Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest;
using Application.Features.SellerInventory.SellerDeals.Commands.CreateDeal;
using Domain.Entities.Requests;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// Represents the requests controller.
/// </summary>
public sealed class RequestsController : ApiController
{
    [HttpPost("addHttpRequest")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHttpRequest(
        [FromBody] string request,
        CancellationToken cancellationToken)
    {
        var command = new CreateApiRequestCommand(request);

        var dealId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(CreateHttpRequest), new { dealId }, dealId);
    }

    [HttpGet("executeHttpRequest/{id:Guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExecuteHttpRequest(
    [FromQuery] Guid id,
    CancellationToken cancellationToken)
    {
        var executionResult = await Sender.Send(new ExecuteHttpRequestCommand(id), cancellationToken);

        return Ok(executionResult);
    }
}