using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
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

        var updatedRequestData = apiRequest.ApiRequestData with
        {
            HttpMethod = command.request.ApiRequestData.HttpMethod,
            Url = command.request.ApiRequestData.Url,
            Headers = command.request.ApiRequestData.Headers,
            Body = command.request.ApiRequestData.Body,
            ExpectedResponse = command.request.ApiRequestData.ExpectedResponse
        };

        apiRequest = apiRequest with
        {
            Name = command.request.Name,
            ApiRequestData = updatedRequestData,
            ModifiedDate = DateTime.UtcNow
        };

        _requestRepository.Update(apiRequest);
        return apiRequest;
    }

}
