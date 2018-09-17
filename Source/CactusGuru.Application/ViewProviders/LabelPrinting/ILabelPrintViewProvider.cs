using System;
using System.Threading.Tasks;

namespace CactusGuru.Application.ViewProviders.LabelPrinting
{
    public interface ILabelPrintViewProvider
    {
        CollectionItemDto GetCollectionItem(Guid id);
        Task<LoadInfoDto> LoadInfoAsync();
        TaxonDto GetTaxon(Guid id);
    }
}
