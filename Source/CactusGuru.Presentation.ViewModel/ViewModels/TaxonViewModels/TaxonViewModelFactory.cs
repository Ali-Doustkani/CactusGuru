using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels
{
    public class TaxonViewModelFactory : IWorkingFactory<TaxonViewModel>
    {
        public TaxonViewModel Create(TransferObjectBase dto)
        {
            return new TaxonViewModel((TaxonDto)dto);
        }
    }
}
