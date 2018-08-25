using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders.LabelPrinting
{
    public class TaxonDto : TransferObjectBase
    {
        public string Name { get; set; }
        public string Genus { get; set; }
        public string Species { get; set; }
    }
}
