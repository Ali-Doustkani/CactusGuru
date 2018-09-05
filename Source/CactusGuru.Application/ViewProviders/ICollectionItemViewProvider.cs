using System;
using System.Collections.Generic;
using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders
{
    public interface ICollectionItemViewProvider : IDataEntryViewProvider
    {
        CollectionItemDto GetCollectionItem(Guid id);
        IEnumerable<TaxonDto> GetTaxa();
        IEnumerable<CollectorDto> GetCollectors();
        IEnumerable<SupplierDto> GetSuppliers();
        IEnumerable<IncomeTypeDto> GetIncomeTypes();
        bool HasSimilarCode(string code);
    }
}
