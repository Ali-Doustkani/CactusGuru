using System;

namespace CactusGuru.Application.Implementation
{
    public abstract class ServiceLocationBase
    {
        public static ServiceLocationBase Instance { get; set; }

        public abstract IServiceLocator Begin();
    }

    public interface IServiceLocator : IDisposable
    {
        T Get<T>();
    }
}
