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
    public class ImageListViewProvider : IImageListViewProvider
    {
        public ImageListViewProvider(IUnitOfWork uow,
            ICollectionItemImageRepository repo,
            AssemblerBase<CollectionItemImage, ImageDto> assembler,
            InstagramPackageMaker fileSaver)
        {
            _uow = uow;
            _repo = repo;
            _assembler = assembler;
            _instagramPackageMaker = fileSaver;
        }

        private readonly IUnitOfWork _uow;
        private readonly ICollectionItemImageRepository _repo;
        private readonly AssemblerBase<CollectionItemImage, ImageDto> _assembler;
        private readonly InstagramPackageMaker _instagramPackageMaker;
        private int _start;

        public bool GetImagesAsync(Action<IEnumerable<ImageDto>> callback)
        {
            var images = _repo.GetByRange(_start, 10);
            var dtos = _assembler.ToDataTransferEntities(images);
            if (dtos.Any())
            {
                callback(dtos);
                _start += 10;
                return true;
            }
            _start = 0;
            return false;
        }

        public void SaveToFiles(IEnumerable<ImageDto> images, string path)
        {
            var domainEntities = ToCollectionItemImages(images);
            _instagramPackageMaker.SaveToZip(domainEntities, path);
            _repo.UpdateSharedOnInstagram(images.Select(x => x.Id));
            _uow.SaveChanges();
        }

        private IEnumerable<CollectionItemImage> ToCollectionItemImages(IEnumerable<ImageDto> images)
        {
            var domainEntities = new List<CollectionItemImage>();
            foreach (var image in images)
            {
                var itemImage = new CollectionItemImage();
                _assembler.FillDomainEntity(itemImage, image);
                domainEntities.Add(itemImage);
            }
            return domainEntities;
        }
    }
}
