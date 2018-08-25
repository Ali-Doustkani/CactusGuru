using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders.ImageGallery
{
    public class CollectionItemDto : TransferObjectBase
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Locality { get; set; }
    }
}
