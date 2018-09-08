using CactusGuru.Infrastructure.EventAggregation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Framework.Collections
{
    public class ObservableBag<T> : ObservableCollection<T>, IChangeableCollection
    {
        public ObservableBag(IEnumerable<T> source,
            Func<T, object> selectId,
            Func<object, bool> acceptorFunc,
            Func<object, T> convertorFunc)
            : base(source)
        {
            _idSelectorFunc = selectId;
            _acceptorFunc = acceptorFunc;
            _convertorFunc = convertorFunc;
        }

        private Func<T, object> _idSelectorFunc;
        private Func<object, bool> _acceptorFunc;
        private Func<object, T> _convertorFunc;

        public void Change(NotificationEventArgs info)
        {
            if (!_acceptorFunc(info.Object)) return;
            var item = _convertorFunc(info.Object);
            if (info.OperationType == OperationType.Add)
                Add(item);
            else if (info.OperationType == OperationType.Delete)
                Remove(Single(item));
            else if (info.OperationType == OperationType.Update)
                Update(item, info);
        }

        private void Update(T item, NotificationEventArgs info)
        {
            var oldItem = Single(item);
            var vm = oldItem as WorkingViewModel;
            if (vm != null)
            {
                vm.InnerObject = (item as WorkingViewModel).InnerObject;
            }
            else
            {
                Remove(oldItem);
                Add(item);
            }
        }

        private T Single(T item)
        {
            return this.Single(x => _idSelectorFunc(x).Equals(_idSelectorFunc(item)));
        }
    }
}
