using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Commands.TestExecuteApiRequest;

internal sealed class TestExecuteApiRequestCommandHandler : ICommandHandler<TestExecuteApiRequestCommand, RequestResult>
{
    private readonly IHttpRequestService _httpRequestService;
    private readonly ICurlParserService _curlParserService;

    public TestExecuteApiRequestCommandHandler(ICurlParserService parserService, IHttpRequestService httpRequestService)
    {
        _curlParserService = parserService;
        _httpRequestService = httpRequestService;
    }


    public async Task<RequestResult> Handle(TestExecuteApiRequestCommand command, CancellationToken cancellationToken)
    {
        var request = _curlParserService.ParseCurlCommand("temp", command.Curl);

        return await _httpRequestService.SendRequestAsync(request);
    }
}