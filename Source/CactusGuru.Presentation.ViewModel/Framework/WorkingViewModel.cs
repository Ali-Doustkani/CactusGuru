using CactusGuru.Application.Common;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public abstract class WorkingViewModel : BaseViewModel
    {
        public WorkingViewModel() { }

        public WorkingViewModel(TransferObjectBase innerObj)
        {
            InnerObject = innerObj;
        }

        private TransferObjectBase _innerObject;
        internal TransferObjectBase InnerObject
        {
            get { return _innerObject; }
            set
            {
                _innerObject = value;
                OnPropertyChanged(string.Empty);
            }
        }

        protected T Inner<T>()
        {
            return (T)(object)InnerObject;
        }
    }
}
