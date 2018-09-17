using CactusGuru.Application.Implementation.Services;
using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageList
{
    public class ImageListViewProvider : ViewProviderBase, IImageListViewProvider
    {
        public Task GetImagesAsync(IProgress<ImageDto> progress)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var assembler = locator.Get<AssemblerBase<CollectionItemImage, ImageDto>>();
                    var reader = locator.Get<ICollectionItemImageRepository>().Reader();
                    while (reader.Read())
                        progress.Report(assembler.ToDataTransferEntity(reader.Value));
                }
            });
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
