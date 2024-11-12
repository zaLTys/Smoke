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

        var clonedRequest = apiRequest with
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            Name = apiRequest.Name + " - Clone"
        };

        return _requestRepository.Create(clonedRequest);
    }
}
