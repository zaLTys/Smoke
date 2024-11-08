using Domain.Entities.Requests;
using Domain.Primitives;
using System.Text;
using System.Text.RegularExpressions;

public class HttpRequestService : IHttpRequestService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpRequestService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<RequestResult> SendRequestAsync(ApiRequest httpRequest)
    {
        var client = _httpClientFactory.CreateClient();
        var requestData = httpRequest.ApiRequestData;

        var requestMessage = new HttpRequestMessage
        {
            Method = new HttpMethod(requestData.HttpMethod.ToString()),
            RequestUri = new Uri(requestData.Url),
        };

        foreach (var header in requestData.Headers)
        {
            requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        if (!string.IsNullOrWhiteSpace(requestData.Body) &&
            (requestData.HttpMethod == HttpMethodType.POST || requestData.HttpMethod == HttpMethodType.PUT))
        {
            requestMessage.Content = new StringContent(requestData.Body, Encoding.UTF8, "application/json");
        }

        try
        {
            using var response = await client.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            var isSuccess = response.IsSuccessStatusCode;

            return new RequestResult(
                RequestId: httpRequest.Id,
                Response: responseContent,
                IsSuccess: isSuccess,
                ErrorMessage: isSuccess ? string.Empty : response.ReasonPhrase ?? "Unknown error"
            );
        }
        catch (Exception ex)
        {
            return new RequestResult(
                RequestId: httpRequest.Id,
                Response: string.Empty,
                IsSuccess: false,
                ErrorMessage: ex.Message
            );
        }
    }


}
