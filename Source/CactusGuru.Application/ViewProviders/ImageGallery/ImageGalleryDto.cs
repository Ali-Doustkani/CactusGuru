using System;
using System.Collections.Generic;

namespace CactusGuru.Application.ViewProviders.ImageGallery
{
    public class ImageGalleryDto
    {
        public ImageGalleryDto()
        {
            Images = new List<ImageDto>();
        }

        public Guid CollectionItemId { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}
