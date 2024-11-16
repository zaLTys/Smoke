using Domain.Abstractions.Repositories;
using Domain.Entities.Requests;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories.InMemory
{
    public class InMemoryRequestRepository : IRequestRepository
    {
        private readonly ConcurrentDictionary<Guid, ApiRequest> _requests = new();

        public ApiRequest Create(ApiRequest apiRequest)
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
                var updatedRequestData = new ApiRequestData(
                     request.ApiRequestData.HttpMethod,
                     request.ApiRequestData.Url ?? existingRequest.ApiRequestData.Url,
                     request.ApiRequestData.Headers ?? existingRequest.ApiRequestData.Headers,
                     request.ApiRequestData.Body ?? existingRequest.ApiRequestData.Body,
                     request.ApiRequestData.ExpectedResponse ?? existingRequest.ApiRequestData.ExpectedResponse);

                existingRequest.Name = request.Name ?? existingRequest.Name;
                existingRequest.ApiRequestData = updatedRequestData;
                existingRequest.ModifiedDate = DateTime.UtcNow;

                return _requests[request.Id];
            }

            throw new KeyNotFoundException("No request with the specified ID was found.");
        }


    }

}
