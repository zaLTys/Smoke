using Domain.Entities.Scenarios;
using Domain.Primitives;

namespace Infrastructure.Services
{
    public class ScenarioBuilder : IScenarioBuilder
    {
        private readonly List<ScenarioStep> _steps = new List<ScenarioStep>();
        private string _name;

        public IScenarioBuilder AddStep(
            Guid requestId,
            RequestType requestType,
            int order,
            List<Guid> dependsOn,
            Dictionary<string, string> mappings = null,
            TimeSpan? timeout = null,
            TimeSpan? delayAfter = null)
        {
            var step = new ScenarioStep
            (
                 Guid.NewGuid(),
                 requestType,
                 requestId,
                 order,
                 dependsOn ?? new List<Guid>(),
                 mappings ?? new Dictionary<string, string>(),
                 timeout,
                 delayAfter
            );

            _steps.Add(step);
            return this;
        }

        public Scenario Build(string name)
        {
            _name = name;
            return new Scenario
            (
                 Guid.NewGuid(),
                 _name,
                 _steps.OrderBy(s => s.Order).ToList(),
                 DateTime.UtcNow,
                 DateTime.UtcNow
            );
        }

        public ScenarioStep GetStep(Guid stepId)
        {
            return _steps.Where(x => x.Id == stepId).FirstOrDefault();
        }

        public IScenarioBuilder RemoveStep(Guid stepId)
        {
            _steps.Remove(GetStep(stepId));
            return this;
        }
    }

}
