using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.CollectionItemList
{
    public class CollectionItemAssembler : AssemblerBase<CollectionItem, CollectionItemDto>
    {
        private readonly IFormatter<CollectionItem> _formatter;

        public CollectionItemAssembler(IFormatter<CollectionItem> formatter)
        {
            _formatter = formatter;
        }

        protected override void FillDataTransferEntityImp(CollectionItemDto dto, CollectionItem domainEntity)
        {
            dto.Code = domainEntity.Code;
            dto.Name = _formatter.Format(domainEntity);
        }

        protected override void FillDomainEntityImp(CollectionItem domainEntity, CollectionItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
