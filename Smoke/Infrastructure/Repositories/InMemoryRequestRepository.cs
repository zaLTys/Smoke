using Domain.Abstractions.Repositories;
using Domain.Entities.Requests;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories
{
    public class InMemoryRequestRepository : IRequestRepository
    {
        private readonly ConcurrentDictionary<Guid, ApiRequest> _requests = new();

        public ApiRequest Save(ApiRequest apiRequest)
        {
            if (_requests.TryAdd(apiRequest.Id, apiRequest))
            {
                return apiRequest;
            }
            else
            {
                throw new InvalidOperationException("A request with this ID already exists.");
            }
        }

        public ApiRequest GetById(Guid requestId)
        {
            if (_requests.TryGetValue(requestId, out var request))
            {
                return request;
            }
            else
            {
                throw new KeyNotFoundException("The request with the specified ID was not found.");
            }
        }

        public IEnumerable<ApiRequest> GetAll()
        {
            return _requests.Values;
        }

        public ApiRequest Update(ApiRequest request)
        {
            if (_requests.TryGetValue(request.Id, out var existingRequest))
            {
                var updatedRequestData = existingRequest.ApiRequestData with
                {
                    HttpMethod = request.ApiRequestData.HttpMethod,
                    Url = request.ApiRequestData.Url ?? existingRequest.ApiRequestData.Url,
                    Headers = request.ApiRequestData.Headers ?? existingRequest.ApiRequestData.Headers,
                    Body = request.ApiRequestData.Body ?? existingRequest.ApiRequestData.Body,
                    ExpectedResponse = request.ApiRequestData.ExpectedResponse ?? existingRequest.ApiRequestData.ExpectedResponse
                };

                var updatedRequest = existingRequest with
                {
                    Name = request.Name ?? existingRequest.Name,
                    ApiRequestData = updatedRequestData,
                    ModifiedDate = DateTime.UtcNow
                };

                _requests[request.Id] = updatedRequest;
                return _requests[request.Id];
            }

            throw new KeyNotFoundException("No request with the specified ID was found.");
        }


    }
}
