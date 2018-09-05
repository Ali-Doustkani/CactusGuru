using CactusGuru.Application.ViewProviders;
using CactusGuru.Entry.Presentation;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.View.NavigationService;
using CactusGuru.Presentation.View.Views.DataEntries;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.Utils;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
            For<INavigationService>().Use<NavigationService>().Singleton();
            For<IDialogService>().Use<DialogService>().Singleton();
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
