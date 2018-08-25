using System.Collections.Generic;

namespace CactusGuru.Infrastructure.Persistance.Merging
{
    public interface IMerger<T>
        where T : DomainEntity
    {
        void Merge(IEnumerable<T> originalItems, IEnumerable<T> currentItems);
    }
}
