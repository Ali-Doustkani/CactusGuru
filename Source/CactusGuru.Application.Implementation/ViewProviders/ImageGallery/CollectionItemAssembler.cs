using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageGallery
{
    public class CollectionItemAssembler: AssemblerBase<CollectionItem, CollectionItemDto>
    {
        protected override void FillDataTransferEntityImp(CollectionItemDto dto, CollectionItem domainEntity)
        {
            dto.Id = domainEntity.Id;
            dto.Code = domainEntity.Code;
            dto.Locality = domainEntity.Locality;
            dto.Title = domainEntity.Format("{GENUS} {taxon}");
        }

        protected override void FillDomainEntityImp(CollectionItem domainEntity, CollectionItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
