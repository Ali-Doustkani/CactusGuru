namespace CactusGuru.Infrastructure.EventAggregation
{
    public delegate void NotificationEventHandler(NotificationEventArgs e);

    public class EventAggregator
    {
        public event NotificationEventHandler Notify;

        public void NotifyOthers(object domainObject, OperationType operationType)
        {
            Notify?.Invoke(new NotificationEventArgs(domainObject, operationType));
        }
    }
}
