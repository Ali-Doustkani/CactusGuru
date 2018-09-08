using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Persistance.Merging;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageGallery
{
    public class ImageGallerySaver
    {
        public ImageGallerySaver(AssemblerBase<CollectionItemImage, ImageDto> assembler,
            ICollectionItemImageRepository repository,
            Publisher<CollectionItemImage> publisher,
            Terminator<CollectionItemImage> terminator,
            IUnitOfWork unitOfWork)
        {
            _assembler = assembler;
            _repository = repository;
            _publisher = publisher;
            _terminator = terminator;
            _unitOfWork = unitOfWork;
        }

        private readonly AssemblerBase<CollectionItemImage, ImageDto> _assembler;
        private readonly ICollectionItemImageRepository _repository;
        private readonly Publisher<CollectionItemImage> _publisher;
        private readonly Terminator<CollectionItemImage> _terminator;
        private readonly IUnitOfWork _unitOfWork;

        public void SaveImageGallery(ImageGalleryDto imageGallery)
        {
            var images = new List<CollectionItemImage>(imageGallery.Images.Count);
            var originals = _repository.GetByCollectionItemId(imageGallery.CollectionItemId);

            foreach (var img in imageGallery.Images)
            {
                var poco = new CollectionItemImage();
                _assembler.FillDomainEntity(poco, img);
                images.Add(poco);
            }

            var merger = new DelegateMerger<CollectionItemImage>(
                e => { AddImage(e, imageGallery.Images.Single(x => x.Id == e.Id)); },
                e => { _publisher.Update(e); },
                DeleteImage
            );
            merger.Merge(originals, images);
            _unitOfWork.SaveChanges();
        }

        private void AddImage(CollectionItemImage collectionItemImage, ImageDto dto)
        {
            _publisher.Add(collectionItemImage);
            _repository.AddFullImage(dto.Id, File.ReadAllBytes(dto.ImagePath));
        }

        private void DeleteImage(CollectionItemImage collectionItemImage)
        {
            _terminator.Terminate(collectionItemImage.Id);
            _repository.DeleteFullImage(collectionItemImage.Id);
        }
    }
}
