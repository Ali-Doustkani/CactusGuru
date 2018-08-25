using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.ImageGallery
{
    public class CollectionItemAssembler: AssemblerBase<CollectionItem, CollectionItemDto>
    {
        public CollectionItemAssembler(IFormatter<CollectionItem> formatter)
        {
            _formatter = formatter;
        }

        private readonly IFormatter<CollectionItem> _formatter;

        protected override void FillDataTransferEntityImp(CollectionItemDto dto, CollectionItem domainEntity)
        {
            dto.Id = domainEntity.Id;
            dto.Code = domainEntity.Code;
            dto.Locality = domainEntity.Locality;
            dto.Title = _formatter.Format(domainEntity);
        }

        protected override void FillDomainEntityImp(CollectionItem domainEntity, CollectionItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
