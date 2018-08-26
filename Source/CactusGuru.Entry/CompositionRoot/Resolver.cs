using CactusGuru.Application.Implementation;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Entry.CompositionRoot.Registries;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot
{
    public class Resolver
    {
        private Container _container;

        public Resolver()
        {
            var registery = new Registry();
            registery.IncludeRegistry<InfrastructureRegistry>();
            registery.IncludeRegistry<DomainRegistry>();
            registery.IncludeRegistry<ApplicationRegistry>();
            registery.IncludeRegistry<PresentationRegistry>();
            _container = new Container(registery);
            
            _container.Configure(cfg =>
            {
                cfg.For<IContext>().Use(ctx => ctx);
                cfg.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AssembliesFromApplicationBaseDirectory(asm => asm.GetName().Name.StartsWith("CactusGuru."));
                    x.ConnectImplementationsToTypesClosing(typeof(TranslatorBase<,>));
                    //x.ConnectImplementationsToTypesClosing(typeof(AssemblerBase<,>));
                    x.ConnectImplementationsToTypesClosing(typeof(IFormatter<>));
                    x.ConnectImplementationsToTypesClosing(typeof(IFactory<>));
                    x.ConnectImplementationsToTypesClosing(typeof(ITerminator<>));
                    x.ConnectImplementationsToTypesClosing(typeof(ValidatorBase<>));
                    x.ConnectImplementationsToTypesClosing(typeof(InquiryBase<>));
                    x.WithDefaultConventions();
                });
            });

            var a = _container.GetInstance<AssemblerBase<Taxon, Application.ViewProviders.LabelPrinting.TaxonDto>>();
        }

        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }
    }
}
