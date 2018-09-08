using CactusGuru.Application.Implementation.Services;
using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageList
{
    public class ImageListViewProvider : ViewProviderBase, IImageListViewProvider
    {
        private int _start;

        public bool GetImagesAsync(Action<IEnumerable<ImageDto>> callback)
        {
            using (var locator = Begin())
            {
                var images = locator.Get<ICollectionItemImageRepository>().GetByRange(_start, 10);
                var dtos = locator.Get<AssemblerBase<CollectionItemImage, ImageDto>>().ToDataTransferEntities(images);
                if (dtos.Any())
                {
                    callback(dtos);
                    _start += 10;
                    return true;
                }
                _start = 0;
                return false;
            }
        }

        public void SaveToFiles(IEnumerable<ImageDto> images, string path)
        {
            using (var locator = Begin())
            {
                var domainEntities = ToCollectionItemImages(images, locator);
                locator.Get<InstagramPackageMaker>().SaveToZip(domainEntities, path);
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
