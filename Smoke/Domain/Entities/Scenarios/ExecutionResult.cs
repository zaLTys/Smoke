namespace Domain.Entities.Scenarios
{
    public record ExecutionResult
    (
        Guid ScenarioId,
        List<ScenarioStepResult> StepResults,
        DateTime StartTime,
        DateTime EndTime,
        bool IsSuccess,
        string Logs
    );
}
