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

        public Task<LoadInfoDto> LoadInfoAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                var result = new LoadInfoDto();
                using (var locator = Begin())
                {
                    var taxa = locator.Get<ITaxonRepository>().GetAll().OrderBy(x => x.Genus.Title);
                    result.Taxa = locator.Get<AssemblerBase<Taxon, TaxonDto>>().ToDataTransferEntities(taxa);

                    var collectors = locator.Get<ICollectorRepository>().GetAll().OrderBy(x => x.FullName);
                    result.Collectors = locator.Get<AssemblerBase<Collector, CollectorDto>>().ToDataTransferEntities(collectors);

                    var suppliers = locator.Get<ISupplierRepository>().GetAll().OrderBy(x => x.FullName);
                    result.Suppliers = locator.Get<AssemblerBase<Supplier, SupplierDto>>().ToDataTransferEntities(suppliers);

                    var incomeTypes = new List<IncomeTypeDto>();
                    incomeTypes.Add(new IncomeTypeDto { Value = (int)IncomeType.Plant });
                    incomeTypes.Add(new IncomeTypeDto { Value = (int)IncomeType.Seed });
                    result.IncomeTypes = incomeTypes;

                    return result;
                }
            });
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
