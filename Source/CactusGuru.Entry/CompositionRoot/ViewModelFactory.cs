using CactusGuru.Presentation.View.Views;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Entry.CompositionRoot
{
    public class ViewModelFactory : IViewModelFactory
    {
        public T Resolve<T>() where T : BaseViewModel
        {
            return ObjectFactory.Instance.GetInstance<T>();
        }
    }
}
