using CactusGuru.Application.ViewProviders;
using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Application.ViewProviders.Main;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Entry.Presentation;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Infrastructure.Utils;
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
using System;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class PresentationRegistry : RegistryBase
    {
        public PresentationRegistry(IResolver resolver)
            : base(resolver)
        {
            For<INavigationService>().Singleton().Use(() => new NavigationService(resolver));
            For<IImageEditor>().Use<ImageEditor>();
            For<IPrintService>().Use<PrintService>();
            For<MainViewModel>().Use<MainViewModel>();
            For<IDialogService>().Singleton().Use<DialogService>();
            For<FirstPage>().Use(CreateFirstPage);
            For<GenusEditor>().Use(CreateGeneraEditor);
            For<TaxonEditor>().Use(CreateTaxonEditor);
            For<SupplierEditor>().Use(CreateSupplierEditor);
            For<CollectorEditor>().Use(CreateCollectorEditor);
            For<ImageGallary>().Use(CreateImageGallary);
            For<CollectionItemEditor>().Use(CreateCollectionItemInserter).Named("forInsert");
            For<CollectionItemEditor>().Use(CreateCollectionItemUpdater).Named("forUpdate");
            For<CollectionItemList>().Use(CreateCollectionItemList);
            For<ImageList>().Use(CreateImageList);
            For<LabelPrint>().Use(CreateLabelPrint);
            For<TransactionEditor>().Use(CreateTransactionEditor);
            For<IFormatter<DateTime>>().Singleton().Use(() => { return new MonthNameDateFormatter(); }).Named("monthName");
            For<IFormatter<DateTime>>().Use(() => { return new MonthNumberDateFormatter(); }).Named("monthNumber");
        }

        private FirstPage CreateFirstPage()
        {
            var ret = new FirstPage();
            ret.DataContext = new FirstPageViewModel(Res<IFirstPageViewProvider>(), Res<INavigationService>());
            return ret;
        }

        private GenusEditor CreateGeneraEditor()
        {
            var ret = new GenusEditor();
            ret.DataContext = new GeneraEditorViewModel(Res<IDataEntryViewProvider>("genus"), Res<IDialogService>(), ret);
            return ret;
        }

        private TaxonEditor CreateTaxonEditor()
        {
            var ret = new TaxonEditor();
            ret.DataContext = new TaxonEditorViewModel(Res<IDataEntryViewProvider>("taxon"), Res<INavigationService>(), Res<IDialogService>(), ret, Res<EventAggregator>());
            return ret;
        }

        private SupplierEditor CreateSupplierEditor()
        {
            var ret = new SupplierEditor();
            ret.DataContext = new SupplierEditorViewModel(Res<IDataEntryViewProvider>("supplier"), Res<IDialogService>(), ret);
            return ret;
        }

        private CollectorEditor CreateCollectorEditor()
        {
            var ret = new CollectorEditor();
            ret.DataContext = new CollectorEditorViewModel(Res<IDataEntryViewProvider>("collector"), Res<IDialogService>(), ret);
            return ret;
        }

        private ImageGallary CreateImageGallary()
        {
            var ret = new ImageGallary();
            ret.DataContext = new ImageGallaryEditorViewModel(Res<IImageGalleryViewProvider>(),
                Res<IDialogService>(),
                new ImageItemViewModelFactory(Res<IFormatter<DateTime>>("monthName")),
                Res<INavigationService>());
            return ret;
        }

        private CollectionItemEditor CreateCollectionItemInserter()
        {
            var ret = new CollectionItemEditor();
            ret.DataContext = new CollectionItemEditorViewModel(Res<ICollectionItemViewProvider>(), Res<IDialogService>(), Res<INavigationService>(), ret, Res<EventAggregator>());
            return ret;
        }

        private CollectionItemEditor CreateCollectionItemUpdater()
        {
            var ret = new CollectionItemEditor();
            ret.DataContext = new CollectionItemEditorViewModel(Res<ICollectionItemViewProvider>(), Res<IDialogService>(), Res<INavigationService>(), ret, Res<EventAggregator>());
            return ret;
        }

        private CollectionItemList CreateCollectionItemList()
        {
            var ret = new CollectionItemList();
            ret.DataContext = new CollectionItemListViewModel(Res<ICollectionItemListViewProvider>(), Res<INavigationService>(), Res<IDialogService>(), Res<EventAggregator>());
            return ret;
        }

        private ImageList CreateImageList()
        {
            var ret = new ImageList();
            ret.DataContext = new ImageListViewModel(Res<IImageListViewProvider>(), new ImageViewModelFactory(Res<IFormatter<DateTime>>("monthName")), Res<IDialogService>());
            return ret;
        }

        private LabelPrint CreateLabelPrint()
        {
            var ret = new LabelPrint();
            ret.DataContext = new CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint.LabelPrintEditorViewModel(Res<ILabelPrintViewProvider>(),
                Res<INavigationService>(),
                Res<IDialogService>(),
                Res<IPrintService>(),
                Res<EventAggregator>());
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
