using System;

namespace CactusGuru.Infrastructure.Persistance
{
    public interface ITerminator<T>
        where T : DomainEntity
    {
        void Terminate(Guid id);
    }
}
