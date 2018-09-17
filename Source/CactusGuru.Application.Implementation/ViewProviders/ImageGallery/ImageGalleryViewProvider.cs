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
using System.Threading.Tasks;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageGallery
{
    public class ImageGalleryViewProvider : ViewProviderBase, IImageGalleryViewProvider
    {
        public Task BuildAsync(IEnumerable<string> files, Guid collectionItemId, IProgress<ImageDto> progress)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var factory = locator.Get<IFactory<CollectionItemImage>>();
                    var assembler = locator.Get<AssemblerBase<CollectionItemImage, ImageDto>>();
                    foreach(var path in files)
                    {
                        var arg = new CollectionItemImageFactoryArg(collectionItemId, path);
                        var image = factory.CreateNew(arg);
                        var dto = assembler.ToDataTransferEntity(image);
                        dto.ImagePath = path;
                        progress.Report(dto);
                    }
                }
            });
        }

        public Task SaveImageGalleryAsync(ImageGalleryDto imageGallery)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    locator.Get<ImageGallerySaver>().SaveImageGallery(imageGallery);
                }
            });
        }

        public Task GetThumbnailsOfAsync(Guid collectionItemId, IProgress<ImageDto> progress)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var imgRepo = locator.Get<ICollectionItemImageRepository>();
                    var assembler = locator.Get<AssemblerBase<CollectionItemImage, ImageDto>>();
                    var ids = imgRepo.GetIdsByCollectionItemId(collectionItemId);
                    foreach (var id in ids)
                    {
                        var image = imgRepo.Get(id);
                        if (image == null) return;
                        progress.Report(assembler.ToDataTransferEntity(image));
                    }
                }
            });
        }

        public CollectionItemDto GetCollectionItem(Guid collectionItemId)
        {
            using (var locator = Begin())
            {
                var collectionItemAssembler = locator.Get<AssemblerBase<CollectionItem, CollectionItemDto>>();
                var item = locator.Get<ICollectionItemRepository>().Get(collectionItemId);
                return collectionItemAssembler.ToDataTransferEntity(item);
            }
        }

        public bool CollectionItemCodeExists(string collectionItemCode)
        {
            using (var locator = Begin())
            {
                return locator.Get<ICollectionItemRepository>().ExistsByCode(collectionItemCode);
            }
        }

        public Guid GetCollectionItemIdByCode(string code)
        {
            using (var locator = Begin())
            {
                return locator.Get<ICollectionItemRepository>().GetIdByCode(code);
            }
        }

        public void SaveToFiles(IEnumerable<ImageDto> images, string directoryPath)
        {
            using (var locator = Begin())
            {
                locator.Get<FileSaver>().SaveToFiles(images, directoryPath);
            }
        }

        public void SaveToZip(IEnumerable<ImageDto> images, string directoryPath)
        {
            using (var locator = Begin())
            {
                var domainEntities = ToCollectionItemImages(images, locator);
                locator.Get<InstagramPackageMaker>().SaveToZip(domainEntities, directoryPath);
                locator.Get<ICollectionItemImageRepository>().UpdateSharedOnInstagram(images.Select(x => x.Id));
                locator.Get<IUnitOfWork>().SaveChanges();
            }
        }

        private IEnumerable<CollectionItemImage> ToCollectionItemImages(IEnumerable<ImageDto> images, IServiceLocator locator)
        {
            var domainEntities = new List<CollectionItemImage>();
            foreach (var image in images)
            {
                var itemImage = new CollectionItemImage();
                locator.Get<AssemblerBase<CollectionItemImage, ImageDto>>().FillDomainEntity(itemImage, image);
                domainEntities.Add(itemImage);
            }
            return domainEntities;

        }
    }
}
