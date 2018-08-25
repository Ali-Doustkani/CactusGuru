using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Domain.Greenhouse.Formatting;
using System;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageListViewModels
{
    public class ImageViewModelFactory
    {
        public ImageViewModelFactory(IFormatter<DateTime> formatter)
        {
            _formatter = formatter;
        }

        private readonly IFormatter<DateTime> _formatter;

        public ImageViewModel Create(ImageDto dto)
        {
            return new ImageViewModel(dto, _formatter);
        }
    }
}
