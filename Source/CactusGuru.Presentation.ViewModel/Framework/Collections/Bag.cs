namespace CactusGuru.Presentation.ViewModel.Framework.Collections
{
    public static class Bag
    {
        public static Builder<T> Of<T>()
        {
            return new Builder<T>();
        }
    }
}
