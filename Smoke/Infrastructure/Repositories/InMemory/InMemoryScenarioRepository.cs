using Domain.Abstractions.Repositories;
using Domain.Entities.Requests;
using Domain.Entities.Scenarios;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories.InMemory
{
    public class InMemoryScenarioRepository : IScenarioRepository
    {
        private readonly ConcurrentDictionary<Guid, Scenario> _scenarios = new();

        public Scenario Save(Scenario scenario)
        {
            if (_scenarios.TryAdd(scenario.Id, scenario))
            {
                return scenario;
            }
            else
            {
                throw new InvalidOperationException("A scenario with this ID already exists.");
            }
        }

        public Scenario GetById(Guid scenarioId)
        {
            if (_scenarios.TryGetValue(scenarioId, out var scenario))
            {
                return scenario;
            }
            else
            {
                throw new KeyNotFoundException("The scenario with the specified ID was not found.");
            }
        }

        public IEnumerable<Scenario> GetAll()
        {
            return _scenarios.Values;
        }

        public Scenario Update(Scenario scenario)
        {
            if (_scenarios.TryGetValue(scenario.Id, out var existingScenario))
            {
                var updatedScenario = existingScenario with
                {
                    Name = scenario.Name ?? existingScenario.Name,
                    Steps = scenario.Steps ?? existingScenario.Steps,
                    ModifiedDate = DateTime.UtcNow
                };

                _scenarios[scenario.Id] = updatedScenario;
                return _scenarios[scenario.Id];
            }

            throw new KeyNotFoundException("No scenario with the specified ID was found.");
        }

        public Scenario AssignStep(Guid requestId, Guid scenarioId, int order)
        {
            if (_scenarios.TryGetValue(scenarioId, out var existingScenario))
            {
                var updatedScenario = existingScenario with
                {
                    ModifiedDate = DateTime.UtcNow
                };
                updatedScenario.Steps.Add(ScenarioStep.Default(requestId, order));

                _scenarios[scenarioId] = updatedScenario;
                return _scenarios[scenarioId];
            }

            throw new KeyNotFoundException("No scenario with the specified ID was found.");

        }
    }

}
