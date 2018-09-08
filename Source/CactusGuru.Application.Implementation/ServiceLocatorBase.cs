namespace CactusGuru.Application.Implementation
{
    public abstract class ServiceLocatorBase
    {
        public static ServiceLocatorBase Instance { get; set; }

        public abstract T GetInstance<T>();
    }
}
