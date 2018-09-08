using CactusGuru.Application.Implementation;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot
{
    public class ServiceLocator : IServiceLocator
    {
        public ServiceLocator(IContainer container)
        {
            _container = container;
        }

        private readonly IContainer _container;

        public T Get<T>()
        {
            return _container.GetInstance<T>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
