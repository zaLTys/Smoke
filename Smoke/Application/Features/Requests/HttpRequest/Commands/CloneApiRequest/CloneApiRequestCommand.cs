using Application.Abstractions.Messaging;

namespace Application.Features.Requests.HttpRequest.Commands.CloneApiRequest
{
    public record CloneApiRequestCommand(Guid Id) : ICommand<Guid>;
}
