namespace Domain.Entities.Scenarios
{
    public record Scenario
    (
        Guid Id,
        string Name,
        List<ScenarioStep> Steps,
        DateTime CreatedDate,
        DateTime ModifiedDate
    );
}
