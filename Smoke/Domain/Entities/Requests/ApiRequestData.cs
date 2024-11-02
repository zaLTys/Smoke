using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public record ApiRequestData(
    HttpMethodType HttpMethod,
    string Url,
    Dictionary<string, string> Headers,
    string Body,
    object ExpectedResponse
);

}
