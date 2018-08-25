using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse.Formatting;
using System;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery
{
    public class ImageItemViewModelFactory
    {
        public ImageItemViewModelFactory(IFormatter<DateTime> formatter)
        {
            _formatter = formatter;
        }

        private readonly IFormatter<DateTime> _formatter;

        public ImageItemViewModel Create(ImageDto dto)
        {
            return new ImageItemViewModel(dto, _formatter);
        }
    }
}
