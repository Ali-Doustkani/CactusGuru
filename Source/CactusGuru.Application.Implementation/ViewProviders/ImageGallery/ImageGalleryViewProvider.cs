using CactusGuru.Application.Implementation.Services;
using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Factories;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageGallery
{
    public class ImageGalleryViewProvider : ViewProviderBase, IImageGalleryViewProvider
    {
        public ImageDto Build(string filePath, Guid collectionItemId)
        {
            var factory = Get<IFactory<CollectionItemImage>>();
            var arg = new CollectionItemImageFactoryArg(collectionItemId, filePath);
            var image = factory.CreateNew(arg);
            return Assembler.ToDataTransferEntity(image);
        }

        public void SaveImageGallery(ImageGalleryDto imageGallery)
        {
            Get<ImageGallerySaver>().SaveImageGallery(imageGallery);
        }

        public void GetThumbnailsOf(Guid collectionItemId, Action<ImageDto> callback)
        {
            var ids = ImageRepository.GetIdsByCollectionItemId(collectionItemId);
            foreach (var id in ids)
            {
                var image = ImageRepository.Get(id);
                if (image == null) return;
                callback(Assembler.ToDataTransferEntity(image));
            }
        }

        public CollectionItemDto GetCollectionItem(Guid collectionItemId)
        {
            var collectionItemAssembler = Get<AssemblerBase<CollectionItem, CollectionItemDto>>();
            var item = ItemRepository.Get(collectionItemId);
            return collectionItemAssembler.ToDataTransferEntity(item);
        }

        public bool CollectionItemCodeExists(string collectionItemCode)
        {
            return ItemRepository.ExistsByCode(collectionItemCode);
        }

        public Guid GetCollectionItemIdByCode(string code)
        {
            return ItemRepository.GetIdByCode(code);
        }

        public void SaveToFiles(IEnumerable<ImageDto> images, string directoryPath)
        {
            Get<FileSaver>().SaveToFiles(images, directoryPath);
        }

        public void SaveToZip(IEnumerable<ImageDto> images, string directoryPath)
        {
            var domainEntities = ToCollectionItemImages(images);
            Get<InstagramPackageMaker>().SaveToZip(domainEntities, directoryPath);
            ImageRepository.UpdateSharedOnInstagram(images.Select(x => x.Id));
            Get<IUnitOfWork>().SaveChanges();
        }

        private IEnumerable<CollectionItemImage> ToCollectionItemImages(IEnumerable<ImageDto> images)
        {
            var domainEntities = new List<CollectionItemImage>();
            foreach (var image in images)
            {
                var itemImage = new CollectionItemImage();
                Assembler.FillDomainEntity(itemImage, image);
                domainEntities.Add(itemImage);
            }
            return domainEntities;
        }

        private AssemblerBase<CollectionItemImage, ImageDto> Assembler
        {
            get { return Get<AssemblerBase<CollectionItemImage, ImageDto>>(); }
        }

        private ICollectionItemRepository ItemRepository
        {
            get { return Get<ICollectionItemRepository>(); }
        }

        private ICollectionItemImageRepository ImageRepository
        {
            get { return Get<ICollectionItemImageRepository>(); }
        }
    }
}
