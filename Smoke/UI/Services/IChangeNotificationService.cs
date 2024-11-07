namespace UI.Services
{
    public interface IChangeNotificationService
    {
        event Action<Guid, bool> ActionToInvoke;

        void InvokeCallbackAction(Guid id, bool enabled);
    }
}