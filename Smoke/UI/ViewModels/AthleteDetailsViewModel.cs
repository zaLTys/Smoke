namespace UI.ViewModels
{
    public class ApiRequestDetailsViewModel : SummaryViewModel
    {
        public long Id { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public bool Premium { get; set; }
        public bool Summit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? ProfileImageUrl { get; set; }

        public Guid? ScenarioId { get; set; } = default!;
        public string? ScenarioName { get; set; } = default!;
    }
}
