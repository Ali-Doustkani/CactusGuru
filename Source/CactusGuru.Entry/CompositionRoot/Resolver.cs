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
            _container = new Container();
            _container.Configure(cfg =>
            {
                cfg.For<IContext>().Use(ctx => ctx);
                cfg.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AssembliesFromApplicationBaseDirectory(asm => asm.GetName().Name.StartsWith("CactusGuru."));
                    x.ConnectImplementationsToTypesClosing(typeof(TranslatorBase<,>));
                    x.ConnectImplementationsToTypesClosing(typeof(AssemblerBase<,>));
                    x.ConnectImplementationsToTypesClosing(typeof(IFormatter<>));
                    x.ConnectImplementationsToTypesClosing(typeof(IFactory<>));
                    x.ConnectImplementationsToTypesClosing(typeof(ITerminator<>));
                    x.ConnectImplementationsToTypesClosing(typeof(ValidatorBase<>));
                    x.ConnectImplementationsToTypesClosing(typeof(InquiryBase<>));
                    x.WithDefaultConventions();
                });
                cfg.AddRegistry<InfrastructureRegistry>();
                cfg.AddRegistry<DomainRegistry>();
                cfg.AddRegistry<ApplicationRegistry>();
                cfg.AddRegistry<PresentationRegistry>();
            });

            var a = _container.GetInstance<AssemblerBase<Taxon, Application.ViewProviders.LabelPrinting.TaxonDto>>();
        }

        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }
    }
}
