using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.UpdateApiRequest;

public record UpdateApiRequestCommand(ApiRequest request) : ICommand<ApiRequest>;
