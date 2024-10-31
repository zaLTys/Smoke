using Application.Abstractions.Messaging;
using Domain.Entities.SellerInventory.SellerDeals;

namespace Application.Features.SellerInventory.SellerDeals.Commands.CreateDeal;

internal sealed class CreateDealCommandHandler : ICommandHandler<CreateDealCommand, string>
{

    public async Task<string> Handle(CreateDealCommand request, CancellationToken cancellationToken)
    {
        var deal = new Deal(Guid.NewGuid().ToString(), request.Name);

        return deal.Id;
    }
}