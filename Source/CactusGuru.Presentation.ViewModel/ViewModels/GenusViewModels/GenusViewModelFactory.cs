using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels
{
    public class GenusViewModelFactory : IWorkingFactory<GenusViewModel>
    {
        public GenusViewModel Create(TransferObjectBase dto)
        {
            return new GenusViewModel((GenusDto)dto);
        }
    }
}
