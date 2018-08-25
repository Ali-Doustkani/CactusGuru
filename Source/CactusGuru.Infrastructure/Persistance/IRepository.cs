using System;
using System.Collections.Generic;

namespace CactusGuru.Infrastructure.Persistance
{
    public interface IRepository<T>
        where T : DomainEntity
    {
        void Add(T domainEntity);
        void Update(T domainEntity);
        void Delete(Guid key);
        T Get(Guid key);
        IEnumerable<T> GetAll();
    }
}
