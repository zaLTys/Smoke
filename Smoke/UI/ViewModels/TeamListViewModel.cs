namespace UI.ViewModels
{
    public class ScenarioListViewModel : SummaryViewModel
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; }
    }
}
