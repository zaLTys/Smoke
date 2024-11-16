using Domain.Primitives;

namespace UI.ViewModels.Scenarios
{
    public class ScenarioStepViewModel
    {
        public Guid Id { get; set; }
        public RequestType StepType { get; set; }
        public Guid RequestId { get; set; }
        public string RequestName { get; set; } = string.Empty;
        public int Order { get; set; }
        public List<Guid> DependsOn { get; set; }
        public Dictionary<string, string> Mappings { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public TimeSpan? DelayAfter { get; set; }
    }
}
