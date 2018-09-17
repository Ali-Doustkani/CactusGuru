using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders
{
    public interface ICollectionItemViewProvider : IDataEntryViewProvider
    {
        CollectionItemDto GetCollectionItem(Guid id);
        Task<IEnumerable<TaxonDto>> GetTaxaAsync();
        Task<IEnumerable<CollectorDto>> GetCollectors();
        Task<IEnumerable<SupplierDto>> GetSuppliers();
        IEnumerable<IncomeTypeDto> GetIncomeTypes();
        bool HasSimilarCode(string code);
    }
}
