using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.ViewModels.TransactionViewModels
{
    public class TransactionEditorViewModel
    {
        public TransactionEditorViewModel()
        {
            Items = new ObservableCollection<TransactionItemViewModel>();
            CollectionItems = new ObservableCollection<CollectionItemViewModel>();
            CollectionItems.Add(new CollectionItemViewModel { Title = "1 - Astrophytum asterias" });
            CollectionItems.Add(new CollectionItemViewModel { Title = "2 - Astrophytum myriostigma" });
            CollectionItems.Add(new CollectionItemViewModel { Title = "3 - Frailea astroides" });
            PartTypes = new ObservableCollection<PartTypeViewModel>();
            PartTypes.Add(new PartTypeViewModel("بذر"));
            PartTypes.Add(new PartTypeViewModel("گیاه"));
        }

        public ObservableCollection<TransactionItemViewModel> Items { get;  }

        public ObservableCollection<CollectionItemViewModel> CollectionItems { get;  }

        public ObservableCollection <PartTypeViewModel > PartTypes { get; }

        public int InitRowNumber()
        {
            return Items.Count;
        }

        public PartTypeViewModel InitPartType()
        {
            return PartTypes.First();
        }

        public bool IsItemValid(TransactionItemViewModel item)
        {
            return item.Amount > 0;
        }

      
    }
}
