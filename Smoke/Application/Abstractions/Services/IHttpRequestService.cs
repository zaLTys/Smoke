using Domain.Entities.Requests;

public interface IHttpRequestService
{
    Task<string> SendRequestAsync(ApiRequest httpRequest);
}
