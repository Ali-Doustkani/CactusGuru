using CactusGuru.Application.Implementation;

namespace CactusGuru.Entry.CompositionRoot
{
    public class ServiceLocator : ServiceLocatorBase
    {
        public override T GetInstance<T>()
        {
            return ObjectFactory.Instance.GetInstance<T>();
        }
    }
}
