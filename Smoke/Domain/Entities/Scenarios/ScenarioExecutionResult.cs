namespace Domain.Entities.Scenarios
{
    public class ScenarioExecutionResult
    {
        public Guid ScenarioId { get; private set; }
        public List<ScenarioStepResult> StepResults { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool IsSuccess { get; private set; }
        public string Logs { get; private set; }

        public ScenarioExecutionResult(Guid scenarioId, List<ScenarioStepResult> stepResults, DateTime startTime, DateTime endTime, bool isSuccess, string logs)
        {
            ScenarioId = scenarioId;
            StepResults = stepResults;
            StartTime = startTime;
            EndTime = endTime;
            IsSuccess = isSuccess;
            Logs = logs;
        }
    }
}
