namespace Domain.Entities.Scenarios
{
    public record Scenario
    (
        Guid Id,
        string Name,
        List<Guid> RequestIds,
        DateTime CreatedDate,
        DateTime ModifiedDate
    );
}
