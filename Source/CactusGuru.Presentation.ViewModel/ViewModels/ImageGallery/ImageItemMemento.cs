using CactusGuru.Application.ViewProviders.ImageGallery;
using System;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery
{
    public class ImageItemMemento
    {
        public ImageItemMemento(ImageDto dto)
        {
            Description = dto.Description;
            DateAdded = dto.DateAdded;
            SharedOnInstagram = dto.SharedOnInstagram;
        }

        public string Description { get; }
        public DateTime DateAdded { get; }
        public bool SharedOnInstagram { get; }
        public bool IsSelected => false;

        public bool IsDirty(ImageDto dto)
        {
            return dto.Description != Description ||
                dto.DateAdded != DateAdded ||
                dto.SharedOnInstagram != SharedOnInstagram;
        }
    }
}
