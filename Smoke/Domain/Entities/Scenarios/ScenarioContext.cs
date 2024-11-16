namespace Domain.Entities.Scenarios
{
    public class ScenarioContext
    {
        public Dictionary<string, object> Variables { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Logs { get; set; }

        public ScenarioContext(Dictionary<string, object> variables, DateTime startTime, DateTime? endTime, bool isSuccess, List<string> logs)
        {
            Variables = variables;
            StartTime = startTime;
            EndTime = endTime;
            IsSuccess = isSuccess;
            Logs = logs;
        }
    }

}
