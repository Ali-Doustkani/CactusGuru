using System.Collections.Generic;

namespace CactusGuru.Application.ViewProviders.LabelPrinting
{
    public class LoadInfoDto
    {
        public IEnumerable<CollectionItemDto> CollectionItems { get; set; }
        public IEnumerable<TaxonDto> Taxa { get; set; }
    }
}
