using CactusGuru.Application.ViewProviders.ImageGallery;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery
{
    public class ImageItemViewModelFactory
    {
        public ImageItemViewModel Create(ImageDto dto)
        {
            return new ImageItemViewModel(dto);
        }
    }
}
