using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactusGuru.Application.ViewProviders.ImageGallery
{
    public interface IImageGalleryViewProvider
    {
        Task BuildAsync(IEnumerable<string> files, Guid collectionItemId, IProgress<ImageDto> progress);
        Task SaveImageGalleryAsync(ImageGalleryDto imageGallery);
        Task GetThumbnailsOfAsync(Guid collectionItemId, IProgress<ImageDto> progress);
        CollectionItemDto GetCollectionItem(Guid collectionItemId);
        bool CollectionItemCodeExists(string collectionItemCode);
        Guid GetCollectionItemIdByCode(string code);
        void SaveToFiles(IEnumerable<ImageDto> images, string directoryPath);
        void SaveToZip(IEnumerable<ImageDto> images, string directoryPath);
    }
}
