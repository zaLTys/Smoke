using Domain.Entities.Requests;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories
{
    public class InMemoryRequestRepository : IRequestRepository
    {
        private readonly ConcurrentDictionary<Guid, ApiRequest> _requests = new();

        public Guid Save(ApiRequest httpRequest)
        {
            if (_requests.TryAdd(httpRequest.Id, httpRequest))
            {
                return httpRequest.Id;
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
                var updatedRequest = existingRequest with
                {
                    Name = request.Name ?? existingRequest.Name,
                    HttpMethod = request.HttpMethod,
                    Url = request.Url ?? existingRequest.Url,
                    Headers = request.Headers ?? existingRequest.Headers,
                    Body = request.Body ?? existingRequest.Body,
                    ExpectedResponse = request.ExpectedResponse ?? existingRequest.ExpectedResponse,
                    ModifiedDate = DateTime.UtcNow
                };

                _requests[request.Id] = updatedRequest;
                return _requests[request.Id];
            }

            throw new KeyNotFoundException("No request with the specified ID was found.");
        }

    }
}
