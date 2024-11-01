using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Queries.GetApiRequest;

internal sealed class GetApiRequestQueryHandler : IQueryHandler<GetApiRequestQuery, ApiRequest>
{
    private readonly IRequestRepository _requestRepository;

    public GetApiRequestQueryHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<ApiRequest> Handle(GetApiRequestQuery query, CancellationToken cancellationToken)
    {
        var apiRequest = _requestRepository.GetById(query.Id);
        if (apiRequest == null)
        {
            throw new KeyNotFoundException($"ApiRequest with ID {query.Id} was not found.");
        }

        return apiRequest;
    }
}
