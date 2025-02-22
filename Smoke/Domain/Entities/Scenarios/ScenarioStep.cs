using Domain.Primitives;

namespace Domain.Entities.Scenarios
{
    public class ScenarioStep
    {
        public Guid Id { get; set; }
        public RequestType StepType { get; set; }
        public Guid RequestId { get; set; }
        public string RequestName { get; set; }
        public int Order { get; set; }
        public List<Guid> DependsOn { get; set; }
        public Dictionary<string, string> Mappings { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public TimeSpan? DelayAfter { get; set; }

        public ScenarioStep(Guid id, RequestType stepType, Guid requestId, string requestName,
            int order, List<Guid> dependsOn,
            Dictionary<string, string> mappings, TimeSpan? timeOut, TimeSpan? delayAfter)
        {
            Id = id;
            StepType = stepType;
            RequestId = requestId;
            RequestName = requestName;
            Order = order;
            DependsOn = dependsOn;
            Mappings = mappings;
            TimeOut = timeOut;
            DelayAfter = delayAfter;
        }

        public static ScenarioStep Default(Guid requestId, int order)
        {
            return new ScenarioStep(
                Guid.NewGuid(),
                RequestType.HttpRequest,
                requestId,
                "PlaceholderRequestName",
                order,
                new List<Guid>(),
                new Dictionary<string, string>(),
                null,
                null);
        }
    }
}