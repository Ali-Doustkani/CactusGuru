using CactusGuru.Application.Common;
using System;

namespace CactusGuru.Application.ViewProviders.ImageGallery
{
    public class ImageDto : TransferObjectBase
    {
        public Guid CollectionItemId { get; set; }
        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public byte[] Thumbnail { get; set; }
        public string ImagePath { get; set; }
        public bool SharedOnInstagram { get; set; }
    }
}
