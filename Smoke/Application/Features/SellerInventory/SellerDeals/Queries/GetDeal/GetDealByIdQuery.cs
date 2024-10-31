using Application.Abstractions.Messaging;

namespace Application.Features.SellerInventory.SellerDeals.Queries.GetDeal;

public sealed record GetDealByIdQuery(string DealId) : IQuery<GetDealByIdResponse>;