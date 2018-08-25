using System;
using System.Collections.Generic;

namespace CactusGuru.Application.ViewProviders.ImageGallery
{
    public interface IImageGalleryViewProvider
    {
        ImageDto Build(string filePath, Guid collectionItemId);
        void SaveImageGallery(ImageGalleryDto imageGallery);
        void GetThumbnailsOf(Guid collectionItemId, Action<ImageDto> callback);
        CollectionItemDto GetCollectionItem(Guid collectionItemId);
        bool CollectionItemCodeExists(string collectionItemCode);
        Guid GetCollectionItemIdByCode(string code);
        void SaveToFiles(IEnumerable<ImageDto> images, string directoryPath);
        void SaveToZip(IEnumerable<ImageDto> images, string directoryPath);
    }
}
