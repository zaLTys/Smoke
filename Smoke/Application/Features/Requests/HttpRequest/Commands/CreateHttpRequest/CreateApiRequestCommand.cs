using Application.Abstractions.Messaging;

namespace Application.Features.Requests.HttpRequest.Commands.CreateHttpRequest
{
    public record CreateApiRequestCommand(string Curl) : ICommand<Guid>;
}
