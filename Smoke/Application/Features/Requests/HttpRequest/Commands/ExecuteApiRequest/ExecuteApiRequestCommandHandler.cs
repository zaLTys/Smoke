using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities.Requests;
using Domain.Exceptions;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteApiRequest;

internal sealed class ExecuteApiRequestCommandHandler : ICommandHandler<ExecuteApiRequestCommand, RequestResult>
{
    private readonly IHttpRequestService _httpRequestService;
    private readonly IRequestRepository _requestRepository;

    public ExecuteApiRequestCommandHandler(IRequestRepository requestRepository, IHttpRequestService httpRequestService)
    {
        _requestRepository = requestRepository;
        _httpRequestService = httpRequestService;
    }


    public async Task<RequestResult> Handle(ExecuteApiRequestCommand command, CancellationToken cancellationToken)
    {
        var request = _requestRepository.GetById(command.RequestId);
        if (request == null)
        {
            throw new RequestNotFoundException(command.RequestId);
        }

        return await _httpRequestService.SendRequestAsync(request);
    }
}