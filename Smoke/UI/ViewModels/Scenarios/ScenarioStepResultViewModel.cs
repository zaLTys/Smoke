namespace UI.ViewModels.Scenarios
{

    public class ScenarioStepResultViewModel
    {
        public Guid StepId { get; set; }
        public object Response { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, string> OutputData { get; set; }
    }
}
