namespace Domain.Entities.Scenarios
{
    public record ScenarioStepResult(
        Guid StepId,
        object Response,
        bool IsSuccess,
        string ErrorMessage,
        Dictionary<string, string> OutputData
    );

}
