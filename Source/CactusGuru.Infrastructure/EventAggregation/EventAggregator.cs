namespace CactusGuru.Infrastructure.EventAggregation
{
    public delegate void NotificationEventHandler(NotificationEventArgs e);

    public class EventAggregator
    {
        public void Subscribe(IListener listener)
        {
            Notify += listener.Notify;
            listener.Disposed += listener_Disposed;
        }

        private void listener_Disposed(IListener listener)
        {
            Notify -= listener.Notify;
        }

        public event NotificationEventHandler Notify = delegate { };

        public void NotifyOthers(object domainObject, OperationType operationType)
        {
            Notify(new NotificationEventArgs(domainObject, operationType));
        }
    }
}
