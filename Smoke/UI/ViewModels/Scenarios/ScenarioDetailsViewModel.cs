using UI.ViewModels.Requests;

namespace UI.ViewModels.Scenarios
{
    public class ScenarioDetailsViewModel
    {
        public Guid Id { get; set; } = default!;

        public string Name { get; set; }

        public List<ApiRequestViewModel> ApiRequests { get; set; }
    }
}
