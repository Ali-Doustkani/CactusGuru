using CactusGuru.Infrastructure;
using StructureMap.Configuration.DSL;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class RegistryBase : Registry
    {
        public RegistryBase(IResolver resolver)
        {
            _resolver = resolver;
        }

        protected readonly IResolver _resolver;

        protected T Res<T>()
        {
            return _resolver.Resolve<T>();
        }

        protected T Res<T>(string key)
        {
            return _resolver.Resolve<T>(key);
        }
    }
}
