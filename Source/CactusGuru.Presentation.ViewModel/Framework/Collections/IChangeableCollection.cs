using CactusGuru.Infrastructure.EventAggregation;

namespace CactusGuru.Presentation.ViewModel.Framework.Collections
{
    public interface IChangeableCollection
    {
        void Change(NotificationEventArgs info);
    }
}
