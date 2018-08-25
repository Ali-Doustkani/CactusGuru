using System;

namespace CactusGuru.Application.Common
{
    public class CollectionItemImageDto : TransferObjectBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }
        public byte[] Content { get; set; }
    }
}
