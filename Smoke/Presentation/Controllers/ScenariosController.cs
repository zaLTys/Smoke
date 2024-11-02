using Application.Features.Requests.HttpRequest.Commands.CloneApiRequest;
using Application.Features.Requests.HttpRequest.Commands.CreateApiRequest;
using Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest;
using Application.Features.Requests.HttpRequest.Commands.UpdateApiRequest;
using Application.Features.Requests.HttpRequest.Queries.GetApiRequest;
using Application.Features.Requests.HttpRequest.Queries.GetApiRequestList;
using Domain.Entities.Requests;
using Domain.Entities.Scenarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// Represents the scenarios controller.
/// </summary>
public sealed class ScenariosController : ApiController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateScenario(
        [FromBody] string scenarioName,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateScenarioCommand(scenarioName), cancellationToken);

        return CreatedAtAction(nameof(CreateScenario), new { result }, result);
    }

    //[HttpPost("update")]
    //[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> UpdateApiRequest(
    //[FromBody] Scenario request,
    //CancellationToken cancellationToken)
    //{
    //    var dealId = await Sender.Send(new UpdateApiRequestCommand(request), cancellationToken);

    //    return CreatedAtAction(nameof(UpdateApiRequest), new { dealId }, dealId);
    //}

    //[HttpGet("execute")]
    //[ProducesResponseType(typeof(RequestResult), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> ExecuteApiRequest(
    //[FromQuery] Guid id,
    //CancellationToken cancellationToken)
    //{
    //    var result = await Sender.Send(new ExecuteApiRequestCommand(id), cancellationToken);

    //    return Ok(result);
    //}

    //[HttpGet("{id:Guid}")]
    //[ProducesResponseType(typeof(ApiRequest), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetApiRequest(
    //    Guid id,
    //CancellationToken cancellationToken)
    //{
    //    var result = await Sender.Send(new GetApiRequestQuery(id), cancellationToken);

    //    return Ok(result);
    //}

    //[HttpGet("all")]
    //[ProducesResponseType(typeof(ApiRequest), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetApiRequests(
    //CancellationToken cancellationToken)
    //{
    //    var result = await Sender.Send(new GetApiRequestListQuery(), cancellationToken);

    //    return Ok(result);
    //}

    //[HttpPost("clone")]
    //[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CloneApiRequest(
    //[FromQuery] Guid id,
    //CancellationToken cancellationToken)
    //{
    //    var result = await Sender.Send(new CloneApiRequestCommand(id), cancellationToken);

    //    return Ok(result);
    //}
}