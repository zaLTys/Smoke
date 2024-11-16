using Domain.Entities.Scenarios;

namespace Application.Abstractions.Services
{
    public interface IExecutionService
    {
        Task<ScenarioExecutionResult> ExecuteScenarioAsync(Scenario scenario);
    }
}
