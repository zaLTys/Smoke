namespace UI.ViewModels
{
    public class ScenarioManagementApiRequestViewModel
    {
        public long ApiRequestId { get; set; } = default!;
        public string Name { get; set; }
        public Guid ScenarioId { get; set; }
        public string? ProfileImageUrl { get; set; }

    }
}
