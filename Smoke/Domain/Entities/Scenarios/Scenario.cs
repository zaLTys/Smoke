namespace Domain.Entities.Scenarios
{
    public class Scenario
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ScenarioStep> Steps { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Scenario(Guid id, string name, List<ScenarioStep> steps, DateTime createdDate, DateTime modifiedDate)
        {
            Id = id;
            Name = name;
            Steps = steps;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public static Scenario Default(string name)
        {
            return new Scenario(
                Guid.NewGuid(),
                name,
                new List<ScenarioStep>(),
                DateTime.UtcNow,
                DateTime.UtcNow
            );
        }
    }
}
