using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class ScenarioNotFoundException : NotFoundException
{
    public ScenarioNotFoundException(Guid scenarioId)
        : base($"The scenario with the identifier {scenarioId} was not found.")
    {
    }
}