using CactusGuru.Entry.CompositionRoot.Registries;
using CactusGuru.Infrastructure;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot
{
    public class Resolver : IResolver
    {
        public Resolver()
        {
            _container = new Container();
            _container.Configure(x => x.For<IResolver>().Use(this));
            _container.Configure(x => x.AddRegistry(new InfrastructureRegistry(this)));
            _container.Configure(x => x.AddRegistry(new PersistanceRegistry(this)));
            _container.Configure(x => x.AddRegistry(new DomainRegistry(this)));
            _container.Configure(x => x.AddRegistry(new ApplicationRegistry(this)));
            _container.Configure(x => x.AddRegistry(new PresentationRegistry(this)));
        }

        private readonly Container _container;

        public T Resolve<T>()
        {
            return _container.GetInstance<T>();
        }

        public T Resolve<T>(string key)
        {
            return _container.GetInstance<T>(key);
        }
    }
}
