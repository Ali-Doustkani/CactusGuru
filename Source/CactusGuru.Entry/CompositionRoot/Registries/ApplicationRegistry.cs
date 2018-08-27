using CactusGuru.Application.Implementation.ViewProviders;
using CactusGuru.Application.ViewProviders;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            For<IDataEntryViewProvider>().Use<GenusViewProvider>().Named("genus");
            For<IDataEntryViewProvider>().Use<TaxonViewProvider>().Named("taxon");
            For<IDataEntryViewProvider>().Use<CollectorViewProvider>().Named("collector");
            For<IDataEntryViewProvider>().Use<SupplierViewProvider>().Named("supplier");
        }
    }
}
