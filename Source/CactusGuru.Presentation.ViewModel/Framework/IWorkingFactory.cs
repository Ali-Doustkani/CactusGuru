using CactusGuru.Application.Common;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public interface IWorkingFactory<out TViewModel>
    {
        TViewModel Create(TransferObjectBase dto);
    }
}
