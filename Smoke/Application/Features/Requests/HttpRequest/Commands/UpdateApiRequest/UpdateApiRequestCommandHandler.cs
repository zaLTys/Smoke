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

        apiRequest.ApiRequestData.HttpMethod = command.request.ApiRequestData.HttpMethod;
        apiRequest.ApiRequestData.Url = command.request.ApiRequestData.Url;
        apiRequest.ApiRequestData.Headers = command.request.ApiRequestData.Headers;
        apiRequest.ApiRequestData.Body = command.request.ApiRequestData.Body;
        apiRequest.ApiRequestData.ExpectedResponse = command.request.ApiRequestData.ExpectedResponse;

        apiRequest.Name = command.request.Name;
        apiRequest.ModifiedDate = DateTime.UtcNow;

        _requestRepository.Update(apiRequest);
        return apiRequest;
    }

}
