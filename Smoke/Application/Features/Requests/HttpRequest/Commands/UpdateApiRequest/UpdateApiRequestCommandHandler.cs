using Application.Abstractions.Messaging;
using Domain.Entities.Requests;
using Domain.Exceptions;

namespace Application.Features.Requests.HttpRequest.Commands.UpdateApiRequest;

internal sealed class UpdateApiRequestCommandHandler : ICommandHandler<UpdateApiRequestCommand, ApiRequest>
{
    private readonly IRequestRepository _requestRepository;

    public UpdateApiRequestCommandHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<ApiRequest> Handle(UpdateApiRequestCommand command, CancellationToken cancellationToken)
    {
        var apiRequest = _requestRepository.GetById(command.request.Id);
        if (apiRequest == null)
        {
            throw new RequestNotFoundException(command.request.Id);
        }

        apiRequest = apiRequest with
        {
            Name = command.request.Name,
            HttpMethod = command.request.HttpMethod,
            Url = command.request.Url,
            Headers = command.request.Headers,
            Body = command.request.Body,
            ExpectedResponse = command.request.ExpectedResponse,
            ModifiedDate = DateTime.UtcNow
        };

        _requestRepository.Update(apiRequest);
        return apiRequest;
    }
}
