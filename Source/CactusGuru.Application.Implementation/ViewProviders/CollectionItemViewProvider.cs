using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class CollectionItemViewProvider : CommonDataEntryViewProvider<CollectionItem, CollectionItemDto>, ICollectionItemViewProvider
    {
        public CollectionItemDto GetCollectionItem(Guid id)
        {
            using (var locator = Begin())
            {
                var item = locator.Get<ICollectionItemRepository>().Get(id);
                return locator.Get<AssemblerBase<CollectionItem, CollectionItemDto>>().ToDataTransferEntity(item);
            }
        }

        public Task<IEnumerable<TaxonDto>> GetTaxaAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var taxa = locator.Get<ITaxonRepository>().GetAll().OrderBy(x => x.Genus.Title);
                    return locator.Get<AssemblerBase<Taxon, TaxonDto>>().ToDataTransferEntities(taxa);
                }
            });
        }

        public Task<IEnumerable<CollectorDto>> GetCollectors()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var collectors = locator.Get<ICollectorRepository>().GetAll().OrderBy(x => x.FullName);
                    return locator.Get<AssemblerBase<Collector, CollectorDto>>().ToDataTransferEntities(collectors);
                }
            });
        }

        public Task<IEnumerable<SupplierDto>> GetSuppliers()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var suppliers = locator.Get<ISupplierRepository>().GetAll().OrderBy(x => x.FullName);
                    return locator.Get<AssemblerBase<Supplier, SupplierDto>>().ToDataTransferEntities(suppliers);
                }
            });
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
            using (var locator = Begin())
            {
                return locator.Get<ICollectionItemRepository>().ExistsByCode(code);
            }
        }
    }
}
