namespace UI.ViewModels.Scenarios
{
    public class ScenarioViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ScenarioStepViewModel> Steps { get; set; } = new List<ScenarioStepViewModel>();
    }
}
