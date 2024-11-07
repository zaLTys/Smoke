namespace UI.Services
{
    public class StateChangeService : IStateChangeService
    {
        public event Action RefreshRequested = default!;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
