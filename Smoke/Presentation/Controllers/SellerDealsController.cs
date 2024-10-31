using Application.Features.SellerInventory.SellerDeals.Commands.CreateDeal;
using Application.Features.SellerInventory.SellerDeals.Queries.GetDeal;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// Represents the deals controller.
/// </summary>
public sealed class SellerDealsController : ApiController
{
    /// <summary>
    /// Gets the deal with the specified identifier, if it exists.
    /// </summary>
    /// <param name="dealId">The deal identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deal with the specified identifier, if it exists.</returns>
    [HttpGet("{dealId:string}")]
    [ProducesResponseType(typeof(GetDealByIdQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDealById(string dealId, CancellationToken cancellationToken)
    {
        var query = new GetDealByIdQuery(dealId);

        var deal = await Sender.Send(query, cancellationToken);

        return Ok(deal);
    }

    /// <summary>
    /// Creates a new deal based on the specified request.
    /// </summary>
    /// <param name="request">The create deal request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The identifier of the newly created deal.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Createdeal(
        [FromBody] CreateDealRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateDealCommand>();

        var dealId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetDealById), new { dealId }, dealId);
    }
}