using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Domain.Greenhouse;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.LabelPrinting
{
    public class CollectionItemAssembler : AssemblerBase<CollectionItem, CollectionItemDto>
    {
        protected override void FillDataTransferEntityImp(CollectionItemDto transferEntity, CollectionItem domainEntity)
        {
            transferEntity.Code = domainEntity.Code;
            transferEntity.Name = domainEntity.Format("{GENUS} {taxon} {field}{, locality}");
            transferEntity.Genus = domainEntity.Format("{GENUS}");
            transferEntity.ReferenceInfo = domainEntity.Format("{ref}");
            transferEntity.Species = domainEntity.Format("{taxon} {field}");
        }

        protected override void FillDomainEntityImp(CollectionItem domainEntity, CollectionItemDto dataTransferentity)
        {
            throw new NotImplementedException();
        }
    }
}
