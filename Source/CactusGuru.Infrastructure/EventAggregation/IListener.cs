using System;

namespace CactusGuru.Infrastructure.EventAggregation
{
    public delegate void DisposeEventHandler(IListener listener);

    public interface IListener : IDisposable
    {
        event DisposeEventHandler Disposed;

        void Notify(NotificationEventArgs e);
    }
}
