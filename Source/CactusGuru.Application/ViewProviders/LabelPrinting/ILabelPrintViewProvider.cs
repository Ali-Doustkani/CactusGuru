using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactusGuru.Application.ViewProviders.LabelPrinting
{
    public interface ILabelPrintViewProvider
    {
        CollectionItemDto GetCollectionItem(Guid id);
        Task<IEnumerable<CollectionItemDto>> GetCollectionItemsAsync();
        TaxonDto GetTaxon(Guid id);
        Task<IEnumerable<TaxonDto>> GetTaxaAsync();
    }
}
