using Application.Abstractions.Messaging;
using System.Data;

namespace Application.Features.SellerInventory.SellerDeals.Queries.GetDeal;

internal sealed class GetDealByIdQueryHandler : IQueryHandler<GetDealByIdQuery, GetDealByIdResponse>
{
    private readonly IDbConnection _dbConnection;

    public GetDealByIdQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<GetDealByIdResponse> Handle(
        GetDealByIdQuery request,
        CancellationToken cancellationToken)
    {

        return new GetDealByIdResponse("1", "name");
    }
}