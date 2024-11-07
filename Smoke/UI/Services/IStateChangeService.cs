namespace UI.Services
{
    public interface IStateChangeService
    {
        event Action RefreshRequested;

        void CallRequestRefresh();
    }
}
