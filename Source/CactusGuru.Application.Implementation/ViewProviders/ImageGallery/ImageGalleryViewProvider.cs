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
    public class ImageGalleryViewProvider : IImageGalleryViewProvider
    {
        public ImageGalleryViewProvider(IUnitOfWork uow,
            AssemblerBase<CollectionItemImage, ImageDto> assembler,
            ICollectionItemImageRepository repository,
            IFactory<CollectionItemImage> factory,
            AssemblerBase<CollectionItem, CollectionItemDto> collectionItemAssembler,
            ICollectionItemRepository collectionItemRepository,
            ImageGallerySaver imageGallerySaver,
            FileSaver fileSaver,
            InstagramPackageMaker instagramPackageMaker)
        {
            _uow = uow;
            _assembler = assembler;
            _repository = repository;
            _factory = factory;
            _collectionItemAssembler = collectionItemAssembler;
            _collectionItemRepository = collectionItemRepository;
            _imageGallerySaver = imageGallerySaver;
            _fileSaver = fileSaver;
            _instagramPackageMaker = instagramPackageMaker;
        }

        private readonly IUnitOfWork _uow;
        private readonly IFactory<CollectionItemImage> _factory;
        private readonly AssemblerBase<CollectionItemImage, ImageDto> _assembler;
        private readonly AssemblerBase<CollectionItem, CollectionItemDto> _collectionItemAssembler;
        private readonly ICollectionItemImageRepository _repository;
        private readonly ICollectionItemRepository _collectionItemRepository;
        private readonly ImageGallerySaver _imageGallerySaver;
        private readonly FileSaver _fileSaver;
        private readonly InstagramPackageMaker _instagramPackageMaker;

        public ImageDto Build(string filePath, Guid collectionItemId)
        {
            var arg = new CollectionItemImageFactoryArg(collectionItemId, filePath);
            var image = _factory.CreateNew(arg);
            return _assembler.ToDataTransferEntity(image);
        }

        public void SaveImageGallery(ImageGalleryDto imageGallery)
        {
            _imageGallerySaver.SaveImageGallery(imageGallery);
        }

        public void GetThumbnailsOf(Guid collectionItemId, Action<ImageDto> callback)
        {
            var ids = _repository.GetIdsByCollectionItemId(collectionItemId);
            foreach (var id in ids)
            {
                var image = _repository.Get(id);
                if (image == null) return;
                callback(_assembler.ToDataTransferEntity(image));
            }
        }

        public CollectionItemDto GetCollectionItem(Guid collectionItemId)
        {
            var item = _collectionItemRepository.Get(collectionItemId);
            return _collectionItemAssembler.ToDataTransferEntity(item);
        }

        public bool CollectionItemCodeExists(string collectionItemCode)
        {
            return _collectionItemRepository.ExistsByCode(collectionItemCode);
        }

        public Guid GetCollectionItemIdByCode(string code)
        {
            return _collectionItemRepository.GetIdByCode(code);
        }

        public void SaveToFiles(IEnumerable<ImageDto> images, string directoryPath)
        {
            _fileSaver.SaveToFiles(images, directoryPath);
        }

        public void SaveToZip(IEnumerable<ImageDto> images, string directoryPath)
        {
            var domainEntities = ToCollectionItemImages(images);
            _instagramPackageMaker.SaveToZip(domainEntities, directoryPath);
            _repository.UpdateSharedOnInstagram(images.Select(x => x.Id));
            _uow.SaveChanges();
        }

        private IEnumerable<CollectionItemImage > ToCollectionItemImages(IEnumerable<ImageDto> images)
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
