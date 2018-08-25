using CactusGuru.Application.Common;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public abstract class WorkingViewModel : BaseViewModel
    {
        protected WorkingViewModel(TransferObjectBase dto)
        {
            InnerObject = dto;
        }

        private TransferObjectBase _innerObject;

        internal TransferObjectBase InnerObject
        {
            get { return _innerObject;}
            set
            {
                _innerObject = value;
                NotifyAll();
            }
        }

        protected T Inner<T>()
        {
            return (T)(object)InnerObject;
        }

        public abstract string FilterTarget { get; }

        protected abstract void NotifyAll();
    }
}
