namespace UI.ViewModels.Scenarios
{
    public class ScenarioContextViewModel
    {
        public Dictionary<string, object> Variables { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Logs { get; set; }

        public ScenarioContextViewModel()
        {
        }
    }
}
