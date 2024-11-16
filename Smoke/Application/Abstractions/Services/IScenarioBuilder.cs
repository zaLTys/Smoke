using Domain.Entities.Scenarios;
using Domain.Primitives;

public interface IScenarioBuilder
{
    IScenarioBuilder AddStep(Guid requestId, RequestType stepType, int order, List<Guid> dependsOn, Dictionary<string, string> mappings = null, TimeSpan? timeout = null, TimeSpan? delayAfter = null);
    IScenarioBuilder RemoveStep(Guid stepId);
    ScenarioStep GetStep(Guid stepId);
    Scenario Build(string name);
}
