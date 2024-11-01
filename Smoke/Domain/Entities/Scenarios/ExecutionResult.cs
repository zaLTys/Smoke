namespace Domain.Entities.Scenarios
{
    public record ExecutionResult
    (
        Guid ScenarioId,
        List<RequestResult> RequestResults,
        DateTime StartTime,
        DateTime EndTime,
        bool IsSuccess,
        string Logs
    );
}
