using Application.Abstractions.Messaging;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteHttpRequest;

internal sealed class ExecuteHttpRequestCommandHandler : ICommandHandler<ExecuteHttpRequestCommand, string>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IHttpRequestService _httpRequestService;

    public ExecuteHttpRequestCommandHandler(IRequestRepository requestRepository, IHttpRequestService httpRequestService)
    {
        _requestRepository = requestRepository;
        _httpRequestService = httpRequestService;
    }


    public async Task<string> Handle(ExecuteHttpRequestCommand command, CancellationToken cancellationToken)
    {
        var httpRequest = _requestRepository.GetById(command.requestId);

        return await _httpRequestService.SendRequestAsync(httpRequest);
    }
}