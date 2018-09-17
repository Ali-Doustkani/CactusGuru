using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Framework.Collections
{
    public static class Bag
    {
        public static Builder<T> Of<T>()
        {
            return new Builder<T>();
        }

        public static ObservableBag<T> Empty<T>()
        {
            return new ObservableBag<T>(Enumerable.Empty<T>(), null, null, null, null);
        }
    }
}
