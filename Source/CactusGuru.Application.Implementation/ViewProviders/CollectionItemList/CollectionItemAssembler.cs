using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Domain.Greenhouse;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.CollectionItemList
{
    public class CollectionItemAssembler : AssemblerBase<CollectionItem, CollectionItemDto>
    {
        protected override void FillDataTransferEntityImp(CollectionItemDto dto, CollectionItem domainEntity)
        {
            dto.Code = domainEntity.Code;
            dto.Name = domainEntity.Format("{GENUS} {taxon} {field}{, locality}");
        }

        protected override void FillDomainEntityImp(CollectionItem domainEntity, CollectionItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
