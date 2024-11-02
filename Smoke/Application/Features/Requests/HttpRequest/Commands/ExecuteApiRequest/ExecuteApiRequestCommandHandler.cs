using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities.Requests;
using Domain.Entities.Scenarios;
using Domain.Exceptions;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest;

internal sealed class ExecuteApiRequestCommandHandler : ICommandHandler<ExecuteApiRequestCommand, RequestResult>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IHttpRequestService _httpRequestService;

    public ExecuteApiRequestCommandHandler(IRequestRepository requestRepository, IHttpRequestService httpRequestService)
    {
        _requestRepository = requestRepository;
        _httpRequestService = httpRequestService;
    }


    public async Task<RequestResult> Handle(ExecuteApiRequestCommand command, CancellationToken cancellationToken)
    {
        var apiRequest = _requestRepository.GetById(command.requestId);
        if (apiRequest == null)
        {
            throw new RequestNotFoundException(command.requestId);
        }

        return  await _httpRequestService.SendRequestAsync(apiRequest);
    }
}