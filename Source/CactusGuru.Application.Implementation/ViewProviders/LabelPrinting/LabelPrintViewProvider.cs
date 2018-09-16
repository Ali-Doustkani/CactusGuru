using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactusGuru.Application.Implementation.ViewProviders.LabelPrinting
{
    public class LabelPrintViewProvider : ViewProviderBase, ILabelPrintViewProvider
    {
        public CollectionItemDto GetCollectionItem(Guid id)
        {
            using (var locator = Begin())
            {
                var item = locator.Get<ICollectionItemRepository>().Get(id);
                return locator.Get<AssemblerBase<CollectionItem, CollectionItemDto>>().ToDataTransferEntity(item);
            }
        }

        public Task<IEnumerable<CollectionItemDto>> GetCollectionItemsAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var items = locator.Get<ICollectionItemRepository>().GetAll();
                    return locator.Get<AssemblerBase<CollectionItem, CollectionItemDto>>().ToDataTransferEntities(items);
                }
            });
        }

        public TaxonDto GetTaxon(Guid id)
        {
            using (var locator = Begin())
            {
                var item = locator.Get<ITaxonRepository>().Get(id);
                return locator.Get<AssemblerBase<Taxon, TaxonDto>>().ToDataTransferEntity(item);
            }
        }

        public Task<IEnumerable<TaxonDto>> GetTaxaAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var items = locator.Get<ITaxonRepository>().GetAll();
                    return locator.Get<AssemblerBase<Taxon, TaxonDto>>().ToDataTransferEntities(items);
                }
            });
        }
    }
}
