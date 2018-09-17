using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System;
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

        public Task<LoadInfoDto> LoadInfoAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var result = new LoadInfoDto();
                    var taxa = locator.Get<ITaxonRepository>().GetAll();
                    result.Taxa = locator.Get<AssemblerBase<Taxon, TaxonDto>>().ToDataTransferEntities(taxa);

                    var collectionItems = locator.Get<ICollectionItemRepository>().GetAll();
                    result.CollectionItems = locator.Get<AssemblerBase<CollectionItem, CollectionItemDto>>().ToDataTransferEntities(collectionItems);

                    return result;
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
    }
}
