namespace UI.ViewModels.Scenarios
{
    public class ScenarioManagementListViewModel
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; }
        public List<ScenarioManagementApiRequestViewModel> ApiRequests { get; set; } = new List<ScenarioManagementApiRequestViewModel>();
    }
}
