using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery
{
    public class GalleryMemento
    {
        public GalleryMemento(IEnumerable<ImageItemViewModel> items)
        {
            _images = new List<ImageItemViewModel>(items.Count());
            foreach (var item in items)
                _images.Add(item);
        }

        private readonly List<ImageItemViewModel> _images;

        public IReadOnlyCollection<ImageItemViewModel> Images => _images;

        public bool AnyDifference(IEnumerable<ImageItemViewModel> items)
        {
            return items.Count() != _images.Count;
        }
    }
}
