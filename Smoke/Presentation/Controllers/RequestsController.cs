using Application.Features.Requests.HttpRequest.Commands.CreateHttpRequest;
using Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest;
using Application.Features.SellerInventory.SellerDeals.Commands.CreateDeal;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// Represents the requests controller.
/// </summary>
public sealed class RequestsController : ApiController
{
    /// <summary>
    /// Creates a new http request on the specified curl request.
    /// </summary>
    /// <param name="request">The save curl request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the newly created http reques.</returns>
    [HttpPost("addHttpRequest")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHttpRequest(
        [FromBody] string request,
        CancellationToken cancellationToken)
    {
        var command = new CreateHttpRequestCommand(request);

        var dealId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(CreateHttpRequest), new { dealId }, dealId);
    }

    [HttpPost("executeHttpRequest")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExecuteHttpRequest(
    [FromBody] ExecuteHttpRequest request,
    CancellationToken cancellationToken)
    {
        var executionResult = await Sender.Send(new ExecuteHttpRequestCommand(request.requestId), cancellationToken);

        return Ok(executionResult);
    }
}