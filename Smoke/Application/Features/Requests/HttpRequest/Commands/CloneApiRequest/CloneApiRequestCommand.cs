using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.CloneApiRequest
{
    public record CloneApiRequestCommand(Guid Id) : ICommand<ApiRequest>;
}
