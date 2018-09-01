using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.Framework.DataSourceManagement
{
    public abstract class DataSourceBase<T> : BaseViewModel, IEnumerable<T>
    {
        protected DataSourceBase()
        {
            Source = new ObservableCollection<T>();
            ClearFilterCommand = new RelayCommand(ClearTextFilter);
            _originalSource = new List<T>();
        }

        private string _filterText;
        protected List<T> _originalSource;

        public ObservableCollection<T> Source { get; set; }

        public ICommand ClearFilterCommand { get; }

        public void ClearTextFilter()
        {
            FilterText = string.Empty;
            OnPropertyChanged(nameof(FilterText));
        }

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Source = new ObservableCollection<T>(_originalSource);
                else
                    Source = new ObservableCollection<T>(Search(value));
                OnPropertyChanged(nameof(Source));
                _filterText = value;
            }
        }

        protected abstract IEnumerable<T> Search(string value);


        public void Load(IEnumerable<T> originalSource)
        {
            _originalSource = new List<T>(originalSource);
            Source = new ObservableCollection<T>(_originalSource);
            OnPropertyChanged(nameof(Source));
        }

        public void Add(T item)
        {
            _originalSource.Add(item);
            Source.Add(item);
        }

        public void Remove(T itemToDelete)
        {
            _originalSource.Remove(itemToDelete);
            Source.Remove(itemToDelete);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
