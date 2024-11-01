using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public record DbRequest
    (
        Guid Id,
        string Name,
        DbType DbType,
        string ConnectionString,
        string Query,
        Dictionary<string, object> Parameters,
        object ExpectedResult,
        StepType Type = StepType.DbRequest
    );
}
