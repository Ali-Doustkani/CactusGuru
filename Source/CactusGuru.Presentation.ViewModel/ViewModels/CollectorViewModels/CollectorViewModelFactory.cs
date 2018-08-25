using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectorViewModels
{
    public class CollectorViewModelFactory : IWorkingFactory<CollectorViewModel>
    {
        public CollectorViewModel Create(TransferObjectBase dto)
        {
            return new CollectorViewModel((CollectorDto)dto);
        }
    }
}
