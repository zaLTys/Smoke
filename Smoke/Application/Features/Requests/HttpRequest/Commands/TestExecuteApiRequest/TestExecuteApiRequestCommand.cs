using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.TestExecuteApiRequest
{
    public record TestExecuteApiRequestCommand(string Curl) : ICommand<RequestResult>;

}
