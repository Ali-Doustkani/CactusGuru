using CactusGuru.Infrastructure.EventAggregation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Framework.Collections
{
    public class ObservableBag<T> : IEnumerable<T>, INotifyCollectionChanged, IChangeableCollection
    {
        public ObservableBag(IEnumerable<T> source,
            Func<T, object> selectId,
            Func<object, bool> acceptorFunc,
            Func<object, T> convertorFunc,
            Func<T, string, bool> filterPredicate)
        {
            _source = source.ToList();
            _filtered = source.ToList();

            _idSelectorFunc = selectId;
            _acceptorFunc = acceptorFunc;
            _convertorFunc = convertorFunc;
            _filterPredicate = filterPredicate;
        }

        private readonly List<T> _source;
        private readonly List<T> _filtered;
        private readonly Func<T, object> _idSelectorFunc;
        private readonly Func<object, bool> _acceptorFunc;
        private readonly Func<object, T> _convertorFunc;
        private readonly Func<T, string, bool> _filterPredicate;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count
        {
            get { return _filtered.Count; }
        }

        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (value == _filterText) return;
                _filterText = value;
                _filtered.Clear();
                _filtered.AddRange(_source.Where(x => _filterPredicate(x, value)));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

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
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, oldItem));
            }
        }

        public void Clear()
        {
            _source.Clear();
            _filtered.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Add(T item)
        {
            _source.Add(item);
            if (FilterDisabled())
                _filtered.Add(item);
            else if (FilterApplies(item))
                _filtered.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void AddRange(IEnumerable<object> items)
        {
            var converted = new List<T>();
            foreach (var item in items)
                converted.Add(_convertorFunc(item));

            _source.AddRange(converted);
            if (FilterDisabled())
                _filtered.AddRange(converted);
            else
            {
                foreach (var item in converted)
                    if (FilterApplies(item))
                        _filtered.Add(item);
            }
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items));
        }

        public void Remove(T item)
        {
            var index = _filtered.IndexOf(item);
            _source.Remove(item);
            if (FilterDisabled())
                _filtered.Remove(item);
            else if (FilterApplies(item))
                _filtered.Remove(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        public void ClearFilterText()
        {
            FilterText = string.Empty;
        }

        private T Single(T item)
        {
            return _source.Single(x => _idSelectorFunc(x).Equals(_idSelectorFunc(item)));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _filtered.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool FilterDisabled()
        {
            return _filterPredicate == null || string.IsNullOrEmpty(_filterText);
        }

        private bool FilterApplies(T item)
        {
            return _filterPredicate(item, _filterText);
        }
    }
}
