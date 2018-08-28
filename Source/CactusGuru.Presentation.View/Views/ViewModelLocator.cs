using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemListViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectorViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery;
using CactusGuru.Presentation.ViewModel.ViewModels.ImageListViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint;
using CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels;

namespace CactusGuru.Presentation.View.Views
{
    public interface IViewModelFactory
    {
        T Resolve<T>() where T : BaseViewModel;
    }

    public class ViewModelLocator
    {
        public static IViewModelFactory Resolver { get; set; }

        public FirstPageViewModel FirstPageViewModel
        {
            get { return Resolver.Resolve<FirstPageViewModel>(); }
        }

        public GenusEditorViewModel GenusEditorViewModel
        {
            get { return Resolver.Resolve<GenusEditorViewModel>(); }
        }

        public CollectionItemEditorViewModel CollectionItemEditorViewModel
        {
            get { return Resolver.Resolve<CollectionItemEditorViewModel>(); }
        }

        public TaxonEditorViewModel TaxonEditorViewModel
        {
            get { return Resolver.Resolve<TaxonEditorViewModel>(); }
        }

        public CollectionItemListViewModel CollectionItemListViewModel
        {
            get { return Resolver.Resolve<CollectionItemListViewModel>(); }
        }

        public CollectorEditorViewModel CollectorEditorViewModel
        {
            get { return Resolver.Resolve<CollectorEditorViewModel>(); }
        }

        public ImageGallaryEditorViewModel ImageGallaryEditorViewModel
        {
            get { return Resolver.Resolve<ImageGallaryEditorViewModel>(); }
        }

        public ImageListViewModel ImageListViewModel
        {
            get { return Resolver.Resolve<ImageListViewModel>(); }
        }

        public LabelPrintEditorViewModel LabelPrintEditorViewModel
        {
            get { return Resolver.Resolve<LabelPrintEditorViewModel>(); }
        }

        public SupplierEditorViewModel SupplierEditorViewModel
        {
            get { return Resolver.Resolve<SupplierEditorViewModel>(); }
        }

        public MainViewModel MainViewModel
        {
            get { return Resolver.Resolve<MainViewModel>(); }
        }
    }
}
