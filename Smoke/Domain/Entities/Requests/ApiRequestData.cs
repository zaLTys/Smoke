using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public class ApiRequestData
    {
        public HttpMethodType HttpMethod { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string? Body { get; set; }
        public object? ExpectedResponse { get; set; }

        public ApiRequestData(HttpMethodType httpMethod, string url, Dictionary<string, string> headers, string? body, object? expectedResponse)
        {
            HttpMethod = httpMethod;
            Url = url;
            Headers = headers;
            Body = body;
            ExpectedResponse = expectedResponse;
        }
    }
}
