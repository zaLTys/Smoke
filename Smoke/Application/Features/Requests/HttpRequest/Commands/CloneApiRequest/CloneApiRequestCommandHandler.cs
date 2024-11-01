using Application.Abstractions.Messaging;
using Application.Features.Requests.HttpRequest.Commands.CloneApiRequest;
using Domain.Exceptions;

namespace Application.Features.Requests.HttpRequest.Commands.CloneHttpRequest;

internal sealed class CloneApiRequestCommandHandler : ICommandHandler<CloneApiRequestCommand, Guid>
{
    private readonly IRequestRepository _requestRepository;

    public CloneApiRequestCommandHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<Guid> Handle(CloneApiRequestCommand command, CancellationToken cancellationToken)
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

        return _requestRepository.Save(clonedRequest);
    }
}
