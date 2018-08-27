using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageList
{
    public class ImageAssembler : AssemblerBase<CollectionItemImage, ImageDto>
    {
        public ImageAssembler(ICollectionItemRepository itemRepo)
        {
            _itemRepo = itemRepo;
        }

        private readonly ICollectionItemRepository _itemRepo;

        protected override void FillDataTransferEntityImp(ImageDto dto, CollectionItemImage domainEntity)
        {
            dto.Thumbnail = domainEntity.Thumbnail;
            dto.Title = _itemRepo.Get(domainEntity.CollectionItemId).Format("{code} - {GENUS} {taxon}");
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
