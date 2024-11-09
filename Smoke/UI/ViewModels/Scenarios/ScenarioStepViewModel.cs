using Domain.Primitives;

namespace UI.ViewModels.Scenarios
{
    public class ScenarioStepViewModel
    {
        public Guid Id { get; set; }
        public StepType StepType { get; set; }
        public Guid RequestId { get; set; }
        public string RequestName { get; set; } = string.Empty;
        public int Order { get; set; }
        public List<Guid> DependsOn { get; set; }
        public Dictionary<string, string> Mappings { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public TimeSpan? DelayAfter { get; set; }

        public ScenarioStepViewModel()
        {
        }

        public static ScenarioStepViewModel Default(Guid requestId, int order)
        {
            return new ScenarioStepViewModel
            {
                Id = Guid.NewGuid(),
                StepType = StepType.HttpRequest,
                RequestId = requestId,
                Order = order,
                DependsOn = new List<Guid>(),
                Mappings = new Dictionary<string, string>(),
                TimeOut = null,
                DelayAfter = null
            };
        }
    }
}
