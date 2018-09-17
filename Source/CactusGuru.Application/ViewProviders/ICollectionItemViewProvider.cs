using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders
{
    public class LoadInfoDto
    {
        public IEnumerable<TaxonDto> Taxa { get; set; }
        public IEnumerable<CollectorDto> Collectors { get; set; }
        public IEnumerable<SupplierDto> Suppliers { get; set; }
        public IEnumerable<IncomeTypeDto> IncomeTypes { get; set; }
    }

    public interface ICollectionItemViewProvider : IDataEntryViewProvider
    {
        CollectionItemDto GetCollectionItem(Guid id);
        Task<LoadInfoDto> LoadInfoAsync();
        bool HasSimilarCode(string code);
    }
}
