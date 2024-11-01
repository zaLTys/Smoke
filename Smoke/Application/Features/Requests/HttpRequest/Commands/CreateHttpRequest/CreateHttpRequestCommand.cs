using Application.Abstractions.Messaging;

namespace Application.Features.Requests.HttpRequest.Commands.CreateHttpRequest
{
    public record CreateHttpRequestCommand(string Curl) : ICommand<Guid>;

}
