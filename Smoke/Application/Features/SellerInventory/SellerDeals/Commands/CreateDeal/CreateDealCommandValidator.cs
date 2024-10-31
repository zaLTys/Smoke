using FluentValidation;

namespace Application.Features.SellerInventory.SellerDeals.Commands.CreateDeal;

public sealed class CreateDealCommandValidator : AbstractValidator<CreateDealCommand>
{
    public CreateDealCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}