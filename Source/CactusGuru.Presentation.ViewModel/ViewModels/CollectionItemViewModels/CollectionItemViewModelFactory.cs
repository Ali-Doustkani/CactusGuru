using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels
{
    class CollectionItemViewModelFactory : IWorkingFactory<CollectionItemViewModel>
    {
        public CollectionItemViewModel Create(TransferObjectBase dto)
        {
            return new CollectionItemViewModel(dto);
        }
    }
}
