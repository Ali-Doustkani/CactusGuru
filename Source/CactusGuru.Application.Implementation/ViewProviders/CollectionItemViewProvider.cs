using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class CollectionItemViewProvider : CommonDataEntryViewProvider<CollectionItem, CollectionItemDto>, ICollectionItemViewProvider
    {
        public CollectionItemDto GetCollectionItem(Guid id)
        {
            var item = Get<ICollectionItemRepository>().Get(id);
            return Get<AssemblerBase<CollectionItem, CollectionItemDto>>().ToDataTransferEntity(item);
        }

        public IEnumerable<TaxonDto> GetTaxa()
        {
            var taxa = Get<ITaxonRepository>().GetAll().OrderBy(x => x.Genus.Title);
            return Get<AssemblerBase<Taxon, TaxonDto>>().ToDataTransferEntities(taxa);
        }

        public IEnumerable<CollectorDto> GetCollectors()
        {
            var collectors = Get<ICollectorRepository>().GetAll().OrderBy(x => x.FullName);
            return Get<AssemblerBase<Collector, CollectorDto>>().ToDataTransferEntities(collectors);
        }

        public IEnumerable<SupplierDto> GetSuppliers()
        {
            var suppliers = Get<ISupplierRepository>().GetAll().OrderBy(x => x.FullName);
            return Get<AssemblerBase<Supplier, SupplierDto>>().ToDataTransferEntities(suppliers);
        }

        public IEnumerable<IncomeTypeDto> GetIncomeTypes()
        {
            var ret = new List<IncomeTypeDto>();
            ret.Add(new IncomeTypeDto { Value = (int)IncomeType.Plant });
            ret.Add(new IncomeTypeDto { Value = (int)IncomeType.Seed });
            return ret;
        }

        public bool HasSimilarCode(string code)
        {
            return Get<ICollectionItemRepository>().ExistsByCode(code);
        }
    }
}
