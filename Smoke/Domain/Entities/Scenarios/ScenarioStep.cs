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
    )
    {
        public static ScenarioStep Default(Guid requestId, int order)
        {
            return new ScenarioStep(
                Id: Guid.NewGuid(),
                StepType: StepType.HttpRequest,
                RequestId: requestId,
                Order: order,
                DependsOn: new List<Guid>(),
                Mappings: new Dictionary<string, string>(),
                TimeOut: null,
                DelayAfter: null
                );
        }
    }

}
