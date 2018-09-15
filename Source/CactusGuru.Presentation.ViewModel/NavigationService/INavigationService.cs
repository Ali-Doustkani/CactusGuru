using System;
using System.Drawing;

namespace CactusGuru.Presentation.ViewModel.NavigationService
{
    public interface INavigationService
    {
        void GotoHome();
        void GotoGenera();
        void GotoTaxa();
        void GotoSuppliers();
        void GotoCollectors();
        void GotoCollectionItemInserter();
        void GotoCollectionItemUpdater(Guid collectionItemToEdit);
        void GotoCollectionItemImageGallary(Guid collectionItem);
        void GotoCollectionItemList();
        void GotoImageList();
        Image SelectImage();
        void CloseCurrentView();
        void GotoLabelPrint();
        DialogResult<DateTime> GetDateFromUser();
    }
}
