using Application.Abstractions.Messaging;

namespace Application.Features.Requests.HttpRequest.Commands.CreateHttpRequest;

internal sealed class CreateApiRequestCommandHandler : ICommandHandler<CreateApiRequestCommand, Guid>
{
    private readonly ICurlParserService _curlParserService;
    private readonly IRequestRepository _requestRepository;

    public CreateApiRequestCommandHandler(ICurlParserService curlParserService, IRequestRepository requestRepository)
    {
        _curlParserService = curlParserService;
        _requestRepository = requestRepository;
    }

    public async Task<Guid> Handle(CreateApiRequestCommand command, CancellationToken cancellationToken)
    {
        var result = _curlParserService.ParseCurlCommand(command.Curl);

        return _requestRepository.Save(result);
    }
}