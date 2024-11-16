using Domain.Abstractions.Repositories;
using Domain.Entities.Requests;
using Domain.Entities.Scenarios;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories.InMemory
{
    public class InMemoryScenarioRepository : IScenarioRepository
    {
        private readonly ConcurrentDictionary<Guid, Scenario> _scenarios = new();

        public Scenario Create(Scenario scenario)
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
                existingScenario.Name = scenario.Name ?? existingScenario.Name;
                existingScenario.Steps = scenario.Steps ?? existingScenario.Steps;
                existingScenario.ModifiedDate = DateTime.UtcNow;

                return existingScenario;
            }

            throw new KeyNotFoundException("No scenario with the specified ID was found.");
        }
    }

}
