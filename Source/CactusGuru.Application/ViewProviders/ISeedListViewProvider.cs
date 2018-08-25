using System;
using System.Collections.Generic;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Sales;

namespace CactusGuru.Application.ViewProviders
{
    public interface ISeedListViewProvider
    {
        SeedList GetSeedList(Guid id);
        IEnumerable<SeedList> GetAllSeedLists();
        SeedList BuildSeedList();
        CollectionSeedListItem BuildCollectionSeedListItem(CollectionItem collectionItem);
        void Add(SeedList seedlist);
        void Update(SeedList seedList);
        IEnumerable<CollectionItem> GetCollectionItems();
        IEnumerable<Supplier> GetSuppliers();
        IEnumerable<Taxon> GetTaxa();
        SeedListItemBase BuildSeedListItem(SeedListItemType type, CollectionItem collectionItem, string code, Taxon taxon);
        string GenerateSupplierSeedCode(IEnumerable<SeedListItemBase> items);
    }
}
