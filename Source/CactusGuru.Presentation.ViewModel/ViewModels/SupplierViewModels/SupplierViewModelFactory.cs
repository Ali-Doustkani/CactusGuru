using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels
{
    public class SupplierViewModelFactory : IWorkingFactory<SupplierViewModel>
    {
        public SupplierViewModel Create(TransferObjectBase dto)
        {
            return new SupplierViewModel((SupplierDto)dto);
        }
    }
}
