namespace UI.ViewModels
{
    public class ScenarioDetailsViewModel
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; }

        public List<ApiRequestListViewModel> ApiRequests { get; set; }
    }
}
