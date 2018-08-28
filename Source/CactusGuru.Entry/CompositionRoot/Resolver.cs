using CactusGuru.Application.Implementation;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Entry.CompositionRoot.Registries;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;
using CactusGuru.Presentation.View.Views;
using CactusGuru.Presentation.ViewModel.Framework;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot
{
    public class Resolver : IViewModelResolver
    {
        private Container _container;

        private Resolver()
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

        public T Resolve<T>() where T : BaseViewModel
        {
            return _container.GetInstance<T>();
        }

        private static Resolver _instance;
        public static Resolver Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Resolver();
                return _instance;
            }
        }
    }
}
