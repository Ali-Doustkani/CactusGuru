using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemListViewModels;
using DevExpress.Xpf.Bars;
using System.Windows;

namespace CactusGuru.Presentation.View.Views
{
    public partial class CollectionItemList 
    {
        public CollectionItemList()
        {
            InitializeComponent();
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel() == null) return;
            ViewModel().EditCurrentCollectionItemCommand.Execute(null);
          //  grid.RefreshRow(grid.View.FocusedRowHandle);
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel() != null)
                ViewModel().DeleteCurrentCollectionItemCommand.Execute(null);
        }

        private void Copy_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel().CopyNameCommand.Execute(null);
        }

        private void GotoImageGallery_OnItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel().GotoImageGallaryCommand.Execute(null);
        }

        private CollectionItemListViewModel ViewModel()
        {
            return (CollectionItemListViewModel)DataContext;
        }
    }
}
