using CactusGuru.Application.ViewProviders.ImageList;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageListViewModels
{
    public class ImageViewModelFactory
    {
        public ImageViewModel Create(ImageDto dto)
        {
            return new ImageViewModel(dto);
        }
    }
}
