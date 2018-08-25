using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders.LabelPrinting
{
    public class CollectionItemDto : TransferObjectBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Genus { get; set; }
        public string Species { get; set; }
        public string ReferenceInfo { get; set; }
    }
}
