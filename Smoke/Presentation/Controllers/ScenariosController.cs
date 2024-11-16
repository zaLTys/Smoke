using Application.Features.Requests.HttpRequest.Commands.CreateApiRequest;
using Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest;
using Application.Features.Requests.HttpRequest.Queries.GetApiRequest;
using Application.Features.Requests.HttpRequest.Queries.GetApiRequestList;
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

    [HttpGet("execute")]
    [ProducesResponseType(typeof(ScenarioExecutionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExecuteScenario(
    [FromQuery] Guid id,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new ExecuteScenarioCommand(id), cancellationToken);

        return Ok(result);
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(ICollection<Scenario>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetScenarios(
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetScenarioListQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(Scenario), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetScenario(
        Guid id,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetScenarioQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpPost("update")]
    [ProducesResponseType(typeof(Scenario), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateScenario(
    [FromBody] UpdateScenarioRequest request,
    CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new UpdateScenarioCommand(request.Scenario), cancellationToken);

        return Ok(result);
    }
}