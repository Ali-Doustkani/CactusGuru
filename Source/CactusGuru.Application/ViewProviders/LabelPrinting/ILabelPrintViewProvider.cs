using System;
using System.Collections.Generic;

namespace CactusGuru.Application.ViewProviders.LabelPrinting
{
    public interface ILabelPrintViewProvider
    {
        CollectionItemDto GetCollectionItem(Guid id);
        IEnumerable<CollectionItemDto> GetCollectionItems();
        TaxonDto GetTaxon(Guid id);
        IEnumerable<TaxonDto> GetTaxa();
    }
}
