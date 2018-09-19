using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Persistance.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageGallery
{
    public class FileSaver
    {
        public FileSaver(ICollectionItemImageRepository collectionItemImageRepository, ICollectionItemRepository collectionItemRepository)
        {
            _collectionItemImageRepository = collectionItemImageRepository;
            _collectionItemRepository = collectionItemRepository;
        }

        private readonly ICollectionItemImageRepository _collectionItemImageRepository;
        private readonly ICollectionItemRepository _collectionItemRepository;

        public void SaveToFiles(IEnumerable<ImageDto> images, string directoryPath)
        {
            for (int i = 0; i < images.Count(); i++)
            {
                var image = images.ElementAt(i);
                var content = _collectionItemImageRepository.GetFullImage(image.Id);
                var path = Path.Combine(directoryPath, CreateName(image, i + 1));
                File.WriteAllBytes(path, content);
            }
        }

        private string CreateName(ImageDto image, int number)
        {
            var collectionItem = _collectionItemRepository.Get(image.CollectionItemId).Format("{code} - {GENUS} {taxon}");
            var date = image.DateAdded.ToString("yyyy-mm-dd");
            var strNumber = number.ToString().PadLeft(2, '0');
            return $"{collectionItem} {date} ({strNumber}).jpg";
        }
    }
}
