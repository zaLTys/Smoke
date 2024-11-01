using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public record ApiRequest
    (
        Guid Id,
        string Name,
        HttpMethodType HttpMethod,
        string Url,
        Dictionary<string, string> Headers,
        string Body,
        object ExpectedResponse,
        DateTime CreatedDate,
        DateTime ModifiedDate,
        StepType Type = StepType.HttpRequest
    );
}
