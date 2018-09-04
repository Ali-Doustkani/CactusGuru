using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint
{
    public class CollectionItemViewModel : WorkingViewModel
    {
        public CollectionItemViewModel(CollectionItemDto innerObject)
            : base(innerObject)
        { }

        public string Code
        {
            get { return Inner<CollectionItemDto>().Code; }
        }

        public string Name
        {
            get { return Inner<CollectionItemDto>().Name; }
        }

        public override string FilterTarget
        {
            get { return Code + Name; }
        }
    }
}
