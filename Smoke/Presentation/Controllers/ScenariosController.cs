using Application.Features.Requests.HttpRequest.Commands.CreateApiRequest;
using Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest;
using Application.Features.Requests.HttpRequest.Queries.GetApiRequest;
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
    [ProducesResponseType(typeof(Scenario), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateScenario(
        [FromBody] CreateScenarioRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateScenarioCommand(request.Name), cancellationToken);

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

    [HttpGet("execute")]
    [ProducesResponseType(typeof(ExecutionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExecuteScenario(
    [FromQuery] Guid id,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new ExecuteScenarioCommand(id), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(Scenario), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetScenarioRequest(
        Guid id,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetScenarioQuery(id), cancellationToken);

        return Ok(result);
    }

    //[HttpGet("all")]
    //[ProducesResponseType(typeof(ApiRequest), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetApiRequests(
    //CancellationToken cancellationToken)
    //{
    //    var result = await Sender.Send(new GetApiRequestListQuery(), cancellationToken);

    //    return Ok(result);
    //}

    [HttpPost("assignStep")]
    [ProducesResponseType(typeof(Scenario), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignStep(
    [FromBody] AssignStepRequest request,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new AssignStepCommand(request.RequestId, request.ScenarioId, request.Order), cancellationToken);

        return Ok(result);
    }
}