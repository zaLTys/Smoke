using Application.Abstractions.Messaging;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest
{
    public record ExecuteApiRequestCommand(Guid requestId) : ICommand<string>;

}
