using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.TransactionViewModels
{
    public class PartTypeViewModel : BaseViewModel
    {
        public PartTypeViewModel(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
}
