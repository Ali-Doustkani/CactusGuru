using System;

namespace CactusGuru.Infrastructure.ObjectCreation
{
    public class SimpleFactory<T> : IFactory<T>
        where T : DomainEntity, new()
    {
        public T CreateNew()
        {
            var ret = new T();
            ret.Id = Guid.NewGuid();
            return ret;
        }

        public T CreateNew(FactoryArg arg)
        {
            return CreateNew();
        }
    }
}
