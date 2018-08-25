using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageGallery
{
    public class CollectionItemImageAssembler : AssemblerBase<CollectionItemImage, ImageDto>
    {
        protected override void FillDataTransferEntityImp(ImageDto dto, CollectionItemImage domainEntity)
        {
            dto.Thumbnail = domainEntity.Thumbnail;
            dto.DateAdded = domainEntity.DateAdded;
            dto.Description = domainEntity.Description;
            dto.Id = domainEntity.Id;
            dto.CollectionItemId = domainEntity.CollectionItemId;
            dto.SharedOnInstagram = domainEntity.SharedOnInstagram;
        }

        protected override void FillDomainEntityImp(CollectionItemImage domainEntity, ImageDto dto)
        {
            domainEntity.Thumbnail = dto.Thumbnail;
            domainEntity.CollectionItemId = dto.CollectionItemId;
            domainEntity.DateAdded = dto.DateAdded;
            domainEntity.Description = dto.Description;
            domainEntity.Id = dto.Id;
            domainEntity.SharedOnInstagram = dto.SharedOnInstagram;
        }
    }
}
