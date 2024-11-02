using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public record ApiRequest
    (
        Guid Id,
        string Name,
        ApiRequestData ApiRequestData,
        DateTime CreatedDate,
        DateTime ModifiedDate,
        StepType Type = StepType.HttpRequest
    );

}
