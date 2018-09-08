using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System;
using System.Collections.Generic;

namespace CactusGuru.Application.Implementation.ViewProviders.LabelPrinting
{
    public class LabelPrintViewProvider : ViewProviderBase, ILabelPrintViewProvider
    {
        public CollectionItemDto GetCollectionItem(Guid id)
        {
            var item = Get<ICollectionItemRepository>().Get(id);
            return Get<AssemblerBase<CollectionItem, CollectionItemDto>>().ToDataTransferEntity(item);
        }

        public IEnumerable<CollectionItemDto> GetCollectionItems()
        {
            var items = Get<ICollectionItemRepository>().GetAll();
            return Get<AssemblerBase<CollectionItem, CollectionItemDto>>().ToDataTransferEntities(items);
        }

        public TaxonDto GetTaxon(Guid id)
        {
            var item = Get<ITaxonRepository>().Get(id);
            return Get<AssemblerBase<Taxon, TaxonDto>>().ToDataTransferEntity(item);
        }

        public IEnumerable<TaxonDto> GetTaxa()
        {
            var items = Get<ITaxonRepository>().GetAll();
            return Get<AssemblerBase<Taxon, TaxonDto>>().ToDataTransferEntities(items);
        }
    }
}
