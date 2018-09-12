using System;
using System.Collections.Generic;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;

namespace CactusGuru.Domain.Persistance.Repositories
{
    public interface ICollectionItemRepository : IRepository<CollectionItem>
    {
        IEnumerable<string> GetAllCodes();
        IEnumerable<CollectionItem> GetBySupplierId(Guid supplierId);
        IEnumerable<CollectionItem> GetByTaxonId(Guid taxonId);
        IEnumerable<CollectionItem> GetByCollectorId(Guid collectorId);
        IEnumerable<CollectionItem> GetByRange(int startIndex, int count);
        IEnumerable<CollectionItem> GetAllSortedByGenus();
        IEnumerable<CollectionItem> GetAllSortedByCode();
        bool ExistsByCode(string code);
        Guid GetIdByCode(string code);
        int GetCount();
    }
}
