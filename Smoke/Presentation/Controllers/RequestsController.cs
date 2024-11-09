using Application.Features.Requests.HttpRequest.Commands.CloneApiRequest;
using Application.Features.Requests.HttpRequest.Commands.CreateApiRequest;
using Application.Features.Requests.HttpRequest.Commands.ExecuteApiRequest;
using Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest;
using Application.Features.Requests.HttpRequest.Commands.UpdateApiRequest;
using Application.Features.Requests.HttpRequest.Queries.GetApiRequest;
using Application.Features.Requests.HttpRequest.Queries.GetApiRequestList;
using Application.Features.SellerInventory.SellerDeals.Commands.CreateDeal;
using Domain.Entities.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// Represents the requests controller.
/// </summary>
public sealed class RequestsController : ApiController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateApiRequest(
        [FromQuery]string name, [FromBody] string curl,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateApiRequestCommand(name, curl), cancellationToken);

        return Ok(result);
    }

    [HttpPost("update")]
    [ProducesResponseType(typeof(ApiRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateApiRequest(
    [FromBody] ApiRequest request,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new UpdateApiRequestCommand(request), cancellationToken);

        return Ok(result);
    }

    [HttpPost("execute")]
    [ProducesResponseType(typeof(RequestResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExecuteApiRequest(
    [FromBody] string curl,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new ExecuteApiRequestCommand(curl), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ApiRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetApiRequest(
        Guid id,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetApiRequestQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(ICollection<ApiRequest>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetApiRequests(
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetApiRequestListQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpPost("clone")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CloneApiRequest(
    [FromQuery] Guid id,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CloneApiRequestCommand(id), cancellationToken);

        return Ok(result);
    }
}