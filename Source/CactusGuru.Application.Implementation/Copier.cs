using System.Reflection;
using CactusGuru.Application.Common;

namespace CactusGuru.Application.Implementation
{
    public class Copier<T>
        where T : TransferObjectBase
    {
        public void Copy(T source, T destination)
        {
            var fields = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var value = field.GetValue(source);
                field.SetValue(destination, value);
            }
        }
    }
}
