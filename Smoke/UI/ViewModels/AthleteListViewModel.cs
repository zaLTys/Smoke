namespace UI.ViewModels
{
    public class ApiRequestListViewModel : SummaryViewModel
    {
        public long Id { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public Guid? ScenarioId { get; set; } 
        public string? ScenarioName { get; set; }
    }
}
