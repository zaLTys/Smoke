namespace Domain.Entities.Scenarios
{
    public record ScenarioContext(
    Dictionary<string, object> Variables,
    DateTime StartTime,
    DateTime? EndTime,
    bool IsSuccess,
    List<string> Logs
);

}
