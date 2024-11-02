using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest
{
    public record ExecuteApiRequestCommand(Guid requestId) : ICommand<RequestResult>;

}
