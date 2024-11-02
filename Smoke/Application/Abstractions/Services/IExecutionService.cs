using Domain.Entities.Scenarios;

namespace Application.Abstractions.Services
{
    public interface IExecutionService
    {
        Task<ExecutionResult> ExecuteScenarioAsync(Scenario scenario);
    }
}
