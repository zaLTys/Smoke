using Application.Abstractions.Messaging;
using Domain.Entities.Requests;


namespace Application.Features.Requests.HttpRequest.Queries.GetApiRequest;

public record GetApiRequestQuery(Guid Id) : IQuery<ApiRequest>;
