using Application.Abstractions.Messaging;
using Application.Features.Requests.HttpRequest.Queries.GetApiRequestList;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Queries.GetApiRequest;

internal sealed class GetApiRequestListQueryHandler : IQueryHandler<GetApiRequestListQuery, IEnumerable<ApiRequest>>
{
    private readonly IRequestRepository _requestRepository;

    public GetApiRequestListQueryHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<IEnumerable<ApiRequest>> Handle(GetApiRequestListQuery query, CancellationToken cancellationToken)
    {
        var result = _requestRepository.GetAll();
        return result;
    }
}
