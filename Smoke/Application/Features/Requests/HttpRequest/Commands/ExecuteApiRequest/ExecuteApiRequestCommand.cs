using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteApiRequest
{
    public record ExecuteApiRequestCommand(Guid RequestId) : ICommand<RequestResult>;

}
