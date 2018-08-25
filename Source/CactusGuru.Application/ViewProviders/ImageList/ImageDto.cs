using CactusGuru.Application.Common;
using System;

namespace CactusGuru.Application.ViewProviders.ImageList
{
    public class ImageDto : TransferObjectBase
    {
        public Guid CollectionItemId { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}
