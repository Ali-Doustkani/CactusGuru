using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    internal class PersistanceRegistry : RegistryBase
    {
        public PersistanceRegistry(IResolver resolver)
            : base(resolver)
        {
            Scan(x =>
            {
                x.AssemblyContainingType<UnitOfWork>();
                x.ConnectImplementationsToTypesClosing(typeof(TranslatorBase<,>));
                x.WithDefaultConventions();
            });

        }
    }
}
