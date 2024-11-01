using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public record AuthRequest : ApiRequest
    {
        public AuthType AuthType { get; init; }
        public object Credentials { get; init; }
        public string TokenEndpoint { get; init; }

        public AuthRequest(
            Guid id,
            string name,
            HttpMethodType httpMethod,
            string url,
            Dictionary<string, string> headers,
            string body,
            object expectedResponse,
            DateTime createdDate,
            DateTime modifiedDate,
            AuthType authType,
            object credentials,
            string tokenEndpoint
        ) : base(id, name, httpMethod, url, headers, body, expectedResponse, createdDate, modifiedDate, StepType.AuthRequest)
        {
            AuthType = authType;
            Credentials = credentials;
            TokenEndpoint = tokenEndpoint;
        }
    }
}
