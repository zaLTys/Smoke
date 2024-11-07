namespace UI.ViewModels.Scenarios
{
    public record ScenarioViewModel
    (
        Guid Id,
        string Name,
        List<ScenarioStepViewModel> Steps,
        DateTime CreatedDate,
        DateTime ModifiedDate
    );

    public record ScenarioStepViewModel
(
    Guid Id,
    string StepType,
    Guid RequestId,
    int Order,
    List<Guid> DependsOn,
    Dictionary<string, string> Mappings,
    TimeSpan? TimeOut,
    TimeSpan? DelayAfter
);
}
