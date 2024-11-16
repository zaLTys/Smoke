using Application.Abstractions.Messaging;
using Application.Features.Requests.HttpRequest.Commands.CloneApiRequest;
using Domain.Abstractions.Repositories;
using Domain.Entities.Requests;
using Domain.Exceptions;

namespace Application.Features.Requests.HttpRequest.Commands.CloneHttpRequest;

internal sealed class CloneApiRequestCommandHandler : ICommandHandler<CloneApiRequestCommand, ApiRequest>
{
    private readonly IRequestRepository _requestRepository;

    public CloneApiRequestCommandHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<ApiRequest> Handle(CloneApiRequestCommand command, CancellationToken cancellationToken)
    {
        var apiRequest = _requestRepository.GetById(command.Id);
        if (apiRequest == null)
        {
            throw new RequestNotFoundException(command.Id);
        }

        var clonedRequest = new ApiRequest(Guid.NewGuid(), apiRequest.Name + " - Clone", apiRequest.ApiRequestData, DateTime.UtcNow, DateTime.UtcNow, apiRequest.Type);

        return _requestRepository.Create(clonedRequest);
    }
}
