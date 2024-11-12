using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.CreateApiRequest;

internal sealed class CreateApiRequestCommandHandler : ICommandHandler<CreateApiRequestCommand, ApiRequest>
{
    private readonly ICurlParserService _curlParserService;
    private readonly IRequestRepository _requestRepository;

    public CreateApiRequestCommandHandler(ICurlParserService curlParserService, IRequestRepository requestRepository)
    {
        _curlParserService = curlParserService;
        _requestRepository = requestRepository;
    }

    public async Task<ApiRequest> Handle(CreateApiRequestCommand command, CancellationToken cancellationToken)
    {
        var result = _curlParserService.ParseCurlCommand(command.Name, command.Curl);

        return _requestRepository.Create(result);
    }
}