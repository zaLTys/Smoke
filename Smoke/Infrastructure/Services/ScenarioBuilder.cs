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
            StepType stepType,
            int order,
            List<Guid> dependsOn,
            Dictionary<string, string> mappings = null,
            TimeSpan? timeout = null,
            TimeSpan? delayAfter = null)
        {
            var step = new ScenarioStep
            (
                Id: Guid.NewGuid(),
                RequestId: requestId,
                StepType: stepType,
                Order: order,
                DependsOn: dependsOn ?? new List<Guid>(),
                Mappings: mappings ?? new Dictionary<string, string>(),
                TimeOut: timeout,
                DelayAfter: delayAfter
            );

            _steps.Add(step);
            return this;
        }

        public Scenario Build(string name)
        {
            _name = name;
            return new Scenario
            (
                Id: Guid.NewGuid(),
                Name: _name,
                Steps: _steps.OrderBy(s => s.Order).ToList(),
                CreatedDate: DateTime.UtcNow,
                ModifiedDate: DateTime.UtcNow
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
