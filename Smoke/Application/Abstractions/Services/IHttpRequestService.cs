using Domain.Entities.Requests;

public interface IHttpRequestService
{
    Task<RequestResult> SendRequestAsync(ApiRequest httpRequest);
}
