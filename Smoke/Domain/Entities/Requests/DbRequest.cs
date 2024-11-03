using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public record DbRequest
    (
        Guid Id,
        string Name,
        DbRequestData DbRequestData,
        DateTime CreatedDate,
        DateTime ModifiedDate,
        StepType Type = StepType.DbRequest
    );

    public record DbRequestData
(
    DbType DbType,
    string ConnectionString,
    string Query,
    Dictionary<string, object> Parameters,
    object ExpectedResult
);

}
