using CactusGuru.Application.ViewProviders;
using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Application.ViewProviders.Main;
using CactusGuru.Entry.Presentation;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.View.NavigationService;
using CactusGuru.Presentation.View.Views;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.PrintService;
using CactusGuru.Presentation.ViewModel.Utils;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemListViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectorViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery;
using CactusGuru.Presentation.ViewModel.ViewModels.ImageListViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.MainViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.TransactionViewModels;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
            For<INavigationService>().Singleton().Use(ctx => new NavigationService(ctx));
            For<IDialogService>().Singleton().Use<DialogService>();
            For<FirstPage>().Use(ctx => CreateFirstPage(ctx));
            For<GenusEditor>().Use(ctx => CreateGeneraEditor(ctx));
            For<TaxonEditor>().Use(ctx => CreateTaxonEditor(ctx));
            For<SupplierEditor>().Use(ctx => CreateSupplierEditor(ctx));
            For<CollectorEditor>().Use(ctx => CreateCollectorEditor(ctx));
            For<ImageGallary>().Use(ctx => CreateImageGallary(ctx));
            For<CollectionItemEditor>().Use(ctx => CreateCollectionItemInserter(ctx)).Named("forInsert");
            For<CollectionItemEditor>().Use(ctx => CreateCollectionItemUpdater(ctx)).Named("forUpdate");
            For<CollectionItemList>().Use(ctx => CreateCollectionItemList(ctx));
            For<ImageList>().Use(ctx => CreateImageList(ctx));
            For<LabelPrint>().Use(ctx => CreateLabelPrint(ctx));
            For<TransactionEditor>().Use(ctx => CreateTransactionEditor());
            For<MonthNameDateFormatter>().Singleton().Use<MonthNameDateFormatter>();
            For<MonthNumberDateFormatter>().Use<MonthNumberDateFormatter>();
        }

        private FirstPage CreateFirstPage(IContext container)
        {
            var ret = new FirstPage();
            ret.DataContext = new FirstPageViewModel(container.GetInstance<IFirstPageViewProvider>(), container.GetInstance<INavigationService>());
            return ret;
        }

        private GenusEditor CreateGeneraEditor(IContext container)
        {
            var ret = new GenusEditor();
            ret.DataContext = new GeneraEditorViewModel(container.GetInstance<IDataEntryViewProvider>("genus"), container.GetInstance<IDialogService>(), ret);
            return ret;
        }

        private TaxonEditor CreateTaxonEditor(IContext container)
        {
            var ret = new TaxonEditor();
            ret.DataContext = new TaxonEditorViewModel(container.GetInstance<IDataEntryViewProvider>("taxon"), container.GetInstance<INavigationService>(), container.GetInstance<IDialogService>(), ret, container.GetInstance<EventAggregator>());
            return ret;
        }

        private SupplierEditor CreateSupplierEditor(IContext container)
        {
            var ret = new SupplierEditor();
            ret.DataContext = new SupplierEditorViewModel(container.GetInstance<IDataEntryViewProvider>("supplier"), container.GetInstance<IDialogService>(), ret);
            return ret;
        }

        private CollectorEditor CreateCollectorEditor(IContext container)
        {
            var ret = new CollectorEditor();
            ret.DataContext = new CollectorEditorViewModel(container.GetInstance<IDataEntryViewProvider>("collector"), container.GetInstance<IDialogService>(), ret);
            return ret;
        }

        private ImageGallary CreateImageGallary(IContext container)
        {
            var ret = new ImageGallary();
            ret.DataContext = new ImageGallaryEditorViewModel(container.GetInstance<IImageGalleryViewProvider>(),
                container.GetInstance<IDialogService>(),
                new ImageItemViewModelFactory( ),
                container.GetInstance<INavigationService>());
            return ret;
        }

        private CollectionItemEditor CreateCollectionItemInserter(IContext container)
        {
            var ret = new CollectionItemEditor();
            ret.DataContext = new CollectionItemEditorViewModel(container.GetInstance<ICollectionItemViewProvider>(), container.GetInstance<IDialogService>(), container.GetInstance<INavigationService>(), ret, container.GetInstance<EventAggregator>());
            return ret;
        }

        private CollectionItemEditor CreateCollectionItemUpdater(IContext container)
        {
            var ret = new CollectionItemEditor();
            ret.DataContext = new CollectionItemEditorViewModel(container.GetInstance<ICollectionItemViewProvider>(), container.GetInstance<IDialogService>(), container.GetInstance<INavigationService>(), ret, container.GetInstance<EventAggregator>());
            return ret;
        }

        private CollectionItemList CreateCollectionItemList(IContext container)
        {
            var ret = new CollectionItemList();
            ret.DataContext = new CollectionItemListViewModel(container.GetInstance<ICollectionItemListViewProvider>(), container.GetInstance<INavigationService>(), container.GetInstance<IDialogService>(), container.GetInstance<EventAggregator>());
            return ret;
        }

        private ImageList CreateImageList(IContext container)
        {
            var ret = new ImageList();
            ret.DataContext = new ImageListViewModel(container.GetInstance<IImageListViewProvider>(), new ImageViewModelFactory( ), container.GetInstance<IDialogService>());
            return ret;
        }

        private LabelPrint CreateLabelPrint(IContext container)
        {
            var ret = new LabelPrint();
            ret.DataContext = new CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint.LabelPrintEditorViewModel(container.GetInstance<ILabelPrintViewProvider>(),
                container.GetInstance<INavigationService>(),
                container.GetInstance<IDialogService>(),
                container.GetInstance<IPrintService>(),
                container.GetInstance<EventAggregator>());
            return ret;
        }

        private TransactionEditor CreateTransactionEditor()
        {
            var ret = new TransactionEditor();
            ret.DataContext = new TransactionEditorViewModel();
            return ret;
        }
    }
}
