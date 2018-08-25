using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Persistance.Repositories;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageList
{
    public class ImageAssembler : AssemblerBase<CollectionItemImage, ImageDto>
    {
        public ImageAssembler(ICollectionItemRepository itemRepo, IFormatter<CollectionItem> formatter)
        {
            _itemRepo = itemRepo;
            _formatter = formatter;
        }

        private readonly ICollectionItemRepository _itemRepo;
        private readonly IFormatter<CollectionItem> _formatter;

        protected override void FillDataTransferEntityImp(ImageDto dto, CollectionItemImage domainEntity)
        {
            dto.Thumbnail = domainEntity.Thumbnail;
            dto.Title = _formatter.Format(_itemRepo.Get(domainEntity.CollectionItemId));
            dto.DateAdded = domainEntity.DateAdded;
            dto.CollectionItemId = domainEntity.CollectionItemId;
        }

        protected override void FillDomainEntityImp(CollectionItemImage domainEntity, ImageDto dto)
        {
            domainEntity.CollectionItemId = dto.CollectionItemId;
            domainEntity.DateAdded = dto.DateAdded;
        }
    }
}
