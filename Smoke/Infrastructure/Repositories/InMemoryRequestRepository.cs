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
    }
}
