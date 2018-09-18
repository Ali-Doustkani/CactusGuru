using System;
using System.Windows.Media.Imaging;

namespace CactusGuru.Presentation.ViewModel.Services.Navigations
{
    public interface INavigationService
    {
        void GotoHome();
        void GotoGenera();
        void GotoTaxa();
        void GotoSuppliers();
        void GotoCollectors();
        void GotoCollectionItem(Guid? collectionItemToEdit = null);
        void GotoCollectionItemImageGallary(Guid collectionItem);
        void GotoCollectionItemList();
        void GotoImageList();
        BitmapImage SelectImage();
        void CloseCurrentView();
        void GotoLabelPrint();
        DialogResult<DateTime> GetDateFromUser(DateTime date);
    }
}
