using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest
{
    public record ExecuteApiRequestCommand(string Curl) : ICommand<RequestResult>;

}
