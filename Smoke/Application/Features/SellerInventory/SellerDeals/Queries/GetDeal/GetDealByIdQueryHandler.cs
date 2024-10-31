using Application.Abstractions.Messaging;
using System.Data;

namespace Application.Features.SellerInventory.SellerDeals.Queries.GetDeal;

internal sealed class GetDealByIdQueryHandler : IQueryHandler<GetDealByIdQuery, GetDealByIdResponse>
{

    public async Task<GetDealByIdResponse> Handle(
        GetDealByIdQuery request,
        CancellationToken cancellationToken)
    {

        return new GetDealByIdResponse("1", "name");
    }
}