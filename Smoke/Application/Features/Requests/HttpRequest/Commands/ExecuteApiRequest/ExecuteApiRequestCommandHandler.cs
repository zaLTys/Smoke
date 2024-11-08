using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.ExecuteApiRequest;

internal sealed class ExecuteApiRequestCommandHandler : ICommandHandler<ExecuteApiRequestCommand, RequestResult>
{
    private readonly IHttpRequestService _httpRequestService;
    private readonly ICurlParserService _curlParserService;

    public ExecuteApiRequestCommandHandler(ICurlParserService parserService, IHttpRequestService httpRequestService)
    {
        _curlParserService = parserService;
        _httpRequestService = httpRequestService;
    }


    public async Task<RequestResult> Handle(ExecuteApiRequestCommand command, CancellationToken cancellationToken)
    {
        var request = _curlParserService.ParseCurlCommand("temp", command.Curl);

        return await _httpRequestService.SendRequestAsync(request);
    }
}