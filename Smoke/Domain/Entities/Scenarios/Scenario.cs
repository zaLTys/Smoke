namespace Domain.Entities.Scenarios
{
    public record Scenario
    (
        Guid Id,
        string Name,
        List<ScenarioStep> Steps,
        DateTime CreatedDate,
        DateTime ModifiedDate
    )
    {
        public static Scenario Default(string name)
        {
            return new Scenario(
                Guid.NewGuid(),
                name,
                new List<ScenarioStep>(),
                DateTime.UtcNow,
                DateTime.UtcNow
            );
        }
    }
}
