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
            dto.Name = domainEntity.Format("{GENUS} {taxon} {field}");
            dto.Info = domainEntity.Format("{locality}");
            dto.TaxonId = domainEntity.Taxon.Id;
            dto.GenusId = domainEntity.Taxon.Genus.Id;
        }

        protected override void FillDomainEntityImp(CollectionItem domainEntity, CollectionItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
