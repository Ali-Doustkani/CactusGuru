using System;

namespace CactusGuru.Infrastructure.EventAggregation
{
    public enum OperationType
    {
        Add,
        Update, 
        Delete
    }

    public class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs(object domainObject, OperationType operationType)
        {
            Object = domainObject;
            OperationType = operationType;
        }

        public object Object { get; private set; }

        public OperationType OperationType { get; private set; }
    }
}
