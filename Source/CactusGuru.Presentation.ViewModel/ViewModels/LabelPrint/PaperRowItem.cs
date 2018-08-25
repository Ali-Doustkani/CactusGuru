using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.PrintService;

namespace CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint
{
    public class PaperRowItem : BaseViewModel
    {
        public PaperRowItem(PaperType id, string title)
        {
            Id = id;
            Title = title;
        }

        public PaperType Id { get; }
        public string Title { get; }
    }
}
