using Application.Abstractions.Messaging;
using Domain.Entities.Requests;

namespace Application.Features.Requests.HttpRequest.Queries.GetApiRequestList;

public record GetApiRequestListQuery() : IQuery<IEnumerable<ApiRequest>>;
