using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CactusGuru.Presentation.ViewModel.Framework.DataSourceManagement
{
    public class DataSource<T> : DataSourceBase<T>
    {
        public DataSource(string propertyName)
        {
            _property = typeof(T).GetProperty(propertyName);
        }

        private readonly PropertyInfo _property;

        protected override IEnumerable<T> Search(string value)
        {
            return _originalSource.Where(x => _property.GetValue(x).ToString().ToLower().Contains(value.ToLower()));
        }
    }
}
