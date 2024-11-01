using Application.Abstractions.Messaging;

namespace Application.Features.Requests.HttpRequest.Commands.CreateApiRequest
{
    public record CreateApiRequestCommand(string Curl) : ICommand<Guid>;
}
