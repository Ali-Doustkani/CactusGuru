using CactusGuru.Application.ViewProviders;
using CactusGuru.Entry.Presentation;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.View.NavigationService;
using CactusGuru.Presentation.View.Views;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.Utils;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectorViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
            For<INavigationService>().Singleton().Use(ctx => new NavigationService(ctx));
            For<IDialogService>().Singleton().Use<DialogService>();
            For<GenusEditorViewModel>().Use<GenusEditorViewModel>().Ctor<IDataEntryViewProvider>().IsNamedInstance("genus");
            For<CollectorEditorViewModel>().Use<CollectorEditorViewModel>().Ctor<IDataEntryViewProvider>().IsNamedInstance("collector");
            For<SupplierEditorViewModel>().Use<SupplierEditorViewModel>().Ctor<IDataEntryViewProvider>().IsNamedInstance("supplier");
            For<TaxonEditorViewModel>().Use<TaxonEditorViewModel>().Ctor<IDataEntryViewProvider>().IsNamedInstance("taxon");
            For<CollectionItemEditor>().Use(ctx => CreateCollectionItemInserter(ctx)).Named("forInsert");
            For<CollectionItemEditor>().Use(ctx => CreateCollectionItemUpdater(ctx)).Named("forUpdate");
            For<MonthNameDateFormatter>().Singleton().Use<MonthNameDateFormatter>();
            For<MonthNumberDateFormatter>().Use<MonthNumberDateFormatter>();
        }

        private CollectionItemEditor CreateCollectionItemInserter(IContext container)
        {
            var ret = new CollectionItemEditor();
            ret.DataContext = new CollectionItemEditorViewModel(container.GetInstance<ICollectionItemViewProvider>(),
                container.GetInstance<IDialogService>(),
                container.GetInstance<INavigationService>(),
              
                container.GetInstance<EventAggregator>());
            return ret;
        }

        private CollectionItemEditor CreateCollectionItemUpdater(IContext container)
        {
            var ret = new CollectionItemEditor();
            ret.DataContext = new CollectionItemEditorViewModel(container.GetInstance<ICollectionItemViewProvider>(),
                container.GetInstance<IDialogService>(), 
                container.GetInstance<INavigationService>(), container.GetInstance<EventAggregator>());
            return ret;
        }
    }
}
