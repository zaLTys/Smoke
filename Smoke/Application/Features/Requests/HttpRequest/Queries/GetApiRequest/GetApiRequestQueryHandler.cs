using Application.Abstractions.Messaging;
using Domain.Abstractions.Repositories;
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
        var result = _requestRepository.GetById(query.Id);
        if (result == null)
        {
            throw new KeyNotFoundException($"ApiRequest with ID {query.Id} was not found.");
        }

        return result;
    }
}
