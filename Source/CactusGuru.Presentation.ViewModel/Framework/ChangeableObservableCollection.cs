using CactusGuru.Application.Common;
using CactusGuru.Infrastructure.EventAggregation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public interface IChangeableCollection
    {
        void Change(NotificationEventArgs info);
    }

    public class ChangeableObservableCollection<T> : ObservableCollection<T>, IChangeableCollection
      where T : TransferObjectBase
    {
        public ChangeableObservableCollection(IEnumerable<T> source)
            : base(source)
        { }

        public void Change(NotificationEventArgs info)
        {
            var item = info.Object as T;
            if (item == null) return;
            if (info.OperationType == OperationType.Add)
                Add(item);
            else if (info.OperationType == OperationType.Delete)
            {
                var oldItem = this.Single(x => x.Id == item.Id);
                Remove(oldItem);
            }
            else if (info.OperationType == OperationType.Update)
            {
                var oldItem = this.Single(x => x.Id == item.Id);
                Remove(oldItem);
                Add(item);
            }
        }
    }
}
