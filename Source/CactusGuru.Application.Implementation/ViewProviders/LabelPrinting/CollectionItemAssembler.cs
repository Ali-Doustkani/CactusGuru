using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.LabelPrinting
{
    public class CollectionItemAssembler : AssemblerBase<CollectionItem, CollectionItemDto>
    {
        public CollectionItemAssembler(IFormatter<CollectionItem> fullNameFormatter,
            IFormatter<CollectionItem> referenceInfoFormatter,
            IFormatter<CollectionItem> itemFormatter)
        {
            _fullNameFormatter = fullNameFormatter;
            _referenceInfoFormatter = referenceInfoFormatter;
            _itemFormatter = itemFormatter;
        }

        private readonly IFormatter<CollectionItem> _fullNameFormatter;
        private readonly IFormatter<CollectionItem> _referenceInfoFormatter;
        private readonly IFormatter<CollectionItem> _itemFormatter;

        protected override void FillDataTransferEntityImp(CollectionItemDto transferEntity, CollectionItem domainEntity)
        {
            transferEntity.Code = domainEntity.Code;
            transferEntity.Name = _fullNameFormatter.Format(domainEntity);
            transferEntity.Genus = domainEntity.Taxon.Genus.ToString("GENUS");
            transferEntity.ReferenceInfo = _referenceInfoFormatter.Format(domainEntity);
            transferEntity.Species = _itemFormatter.Format(domainEntity);
        }

        protected override void FillDomainEntityImp(CollectionItem domainEntity, CollectionItemDto dataTransferentity)
        {
            throw new NotImplementedException();
        }
    }
}
