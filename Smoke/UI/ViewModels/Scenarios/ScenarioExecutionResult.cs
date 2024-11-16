namespace UI.ViewModels.Scenarios
{
    public class ScenarioExecutionResultViewModel
    {
        public Guid ScenarioId { get; private set; }
        public List<ScenarioStepResultViewModel> StepResults { get; private set; }
        public DateTimeOffset StartTime { get; private set; }
        public DateTimeOffset EndTime { get; private set; }
        public bool IsSuccess { get; private set; }
        public string Logs { get; private set; }
    }
}
