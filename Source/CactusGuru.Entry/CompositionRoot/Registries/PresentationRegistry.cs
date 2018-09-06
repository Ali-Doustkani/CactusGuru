using CactusGuru.Entry.Presentation;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.View.NavigationService;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.Utils;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
            Policies.SetAllProperties(policy => policy.OfType<EventAggregator>());
            For<INavigationService>().Use<NavigationService>().Singleton();
            For<IDialogService>().Use<DialogService>().Singleton();
            For<MonthNameDateFormatter>().Singleton().Use<MonthNameDateFormatter>();
            For<MonthNumberDateFormatter>().Use<MonthNumberDateFormatter>();
        }
    }
}
