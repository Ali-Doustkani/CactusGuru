using CactusGuru.Entry.Presentation;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Services.Navigations;
using CactusGuru.Presentation.ViewModel.Tools;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
            Policies.SetAllProperties(policy =>
            {
                policy.OfType<EventAggregator>();
                policy.OfType<IDialogService>();
                policy.OfType<INavigationService>();
            });

            For<INavigationService>().Use<NavigationService>().Singleton();
            For<IDialogService>().Use<DialogService>().Singleton();
            For<MonthNameDateFormatter>().Singleton().Use<MonthNameDateFormatter>();
        }
    }
}
