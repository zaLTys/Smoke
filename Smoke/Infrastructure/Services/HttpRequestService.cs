using Domain.Entities.Requests;
using Domain.Primitives;
using System.Text;

public class HttpRequestService : IHttpRequestService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpRequestService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> SendRequestAsync(ApiRequest httpRequest)
    {
        var client = _httpClientFactory.CreateClient();

        var requestMessage = new HttpRequestMessage
        {
            Method = new HttpMethod(httpRequest.HttpMethod.ToString()),
            RequestUri = new Uri(httpRequest.Url),
        };

        foreach (var header in httpRequest.Headers)
        {
            requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        if (!string.IsNullOrWhiteSpace(httpRequest.Body) && (httpRequest.HttpMethod == HttpMethodType.POST || httpRequest.HttpMethod == HttpMethodType.PUT))
        {
            requestMessage.Content = new StringContent(httpRequest.Body, Encoding.UTF8, "application/json");
        }

        using var response = await client.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
