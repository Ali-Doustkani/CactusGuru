using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CactusGuru.Presentation.ViewModel.Framework.DataSourceManagement
{
    public interface IDataSource<T> : IEnumerable<T>, INotifyPropertyChanged
    {
        ObservableCollection<T> Source { get; set; }
        void ClearTextFilter();
        string FilterText { get; set; }
        void Load(IEnumerable<T> originalSource);
        void Add(T item);
        void Remove(T itemToDelete);
    }
}
