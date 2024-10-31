using System;
using Application.Abstractions.Messaging;

namespace Application.Features.SellerInventory.SellerDeals.Commands.CreateDeal;

public sealed record CreateDealCommand(string Name) : ICommand<string>;