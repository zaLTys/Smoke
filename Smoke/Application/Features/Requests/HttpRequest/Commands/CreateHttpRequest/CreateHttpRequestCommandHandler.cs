using Application.Abstractions.Messaging;

namespace Application.Features.Requests.HttpRequest.Commands.CreateHttpRequest;

internal sealed class CreateHttpRequestCommandHandler : ICommandHandler<CreateHttpRequestCommand, Guid>
{
    private readonly ICurlParserService _curlParserService;
    private readonly IRequestRepository _requestRepository;

    public CreateHttpRequestCommandHandler(ICurlParserService curlParserService, IRequestRepository requestRepository)
    {
        _curlParserService = curlParserService;
        _requestRepository = requestRepository;
    }

    public async Task<Guid> Handle(CreateHttpRequestCommand command, CancellationToken cancellationToken)
    {
        var result = _curlParserService.ParseCurlCommand(command.Curl);

        return _requestRepository.Save(result);
    }
}