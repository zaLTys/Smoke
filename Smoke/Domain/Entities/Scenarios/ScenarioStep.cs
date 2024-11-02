using Domain.Primitives;

namespace Domain.Entities.Scenarios
{
    public record ScenarioStep
    (
        Guid Id,
        StepType StepType,
        Guid RequestId,
        int Order,
        List<Guid> DependsOn,
        Dictionary<string, string> Mappings,
        TimeSpan? TimeOut,
        TimeSpan? DelayAfter
    );

}
