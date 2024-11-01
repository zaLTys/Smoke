using Application.Abstractions.Messaging;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest
{
    public record ExecuteHttpRequestCommand(Guid requestId) : ICommand<string>;

}
