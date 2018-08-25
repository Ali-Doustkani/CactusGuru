using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Framework.DataSourceManagement
{
    public class FilterDataSource<T> : DataSourceBase<T>
        where T : WorkingViewModel
    {
        protected override IEnumerable<T> Search(string value)
        {
            return _originalSource.Where(x => x.FilterTarget.ToLower().Contains(value.ToLower()));
        }
    }
}
