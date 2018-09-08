using CactusGuru.Application.Implementation;
using CactusGuru.Entry.CompositionRoot.Registries;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot
{
    public class ObjectFactory: ServiceLocationBase
    {
        private Container _container;

        private ObjectFactory()
        {
             _container = new Container(); 
            _container.Configure(cfg =>
            {
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
                cfg.AddRegistry<PresentationRegistry>();
            });
        }

        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }

        public T GetInstance<T>(string name)
        {
            return _container.GetInstance<T>(name);
        }

        public override IServiceLocator Begin()
        {
            return new ServiceLocator(_container.GetNestedContainer());
        }

        private static ObjectFactory _instance;
        public static ObjectFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ObjectFactory();
                return _instance;
            }
        }
    }
}
