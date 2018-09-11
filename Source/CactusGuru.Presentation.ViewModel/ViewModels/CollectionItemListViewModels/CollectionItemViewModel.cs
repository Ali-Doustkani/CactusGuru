using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemListViewModels
{
    public class CollectionItemViewModel : WorkingViewModel
    {
        public CollectionItemViewModel(CollectionItemDto dto)
         : base(dto)
        { }

        public override string FilterTarget => Code + Name;

        public string Code
        {
            get { return Inner<CollectionItemDto>().Code; }
            set { Inner<CollectionItemDto>().Code = value; }
        }

        public string Name
        {
            get { return Inner<CollectionItemDto>().Name; }
            set { Inner<CollectionItemDto>().Name = value; }
        }

        public string Info
        {
            get { return Inner<CollectionItemDto>().Info; }
            set { Inner<CollectionItemDto>().Info = value; }
        }
    }
}
