namespace UI.Services
{
    public class ChangeNotificationService : IChangeNotificationService
    {
        public event Action<Guid, bool> ActionToInvoke = default!;

        public void InvokeCallbackAction(Guid id, bool enabled)
        {
            ActionToInvoke?.Invoke(id, enabled);
        }
    }
}
