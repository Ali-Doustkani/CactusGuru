using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Sales;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;
using System;

namespace CactusGuru.Persistance.Translators
{
    public class SeedListTranslator : TranslatorBase<SeedList, tblSeedList>
    {
        public SeedListTranslator(
            TranslatorBase<CollectionItem, tblCollectionItem> collectionItemTranslator,
            TranslatorBase<Taxon, tblTaxon> taxonTranslator,
            TranslatorBase<Supplier, tblSupplier> supplierTranslator)
        {
            _collectionItemTranslator = collectionItemTranslator;
            _taxonTranslator = taxonTranslator;
            _supplierTranslator = supplierTranslator;
        }

        private readonly TranslatorBase<CollectionItem, tblCollectionItem> _collectionItemTranslator;
        private readonly TranslatorBase<Taxon, tblTaxon> _taxonTranslator;
        private readonly TranslatorBase<Supplier, tblSupplier> _supplierTranslator;

        public override void FillDataEntity(tblSeedList dataEntity, SeedList domainEntity)
        {
            dataEntity.Id = domainEntity.Id;
            dataEntity.Name = domainEntity.Name;
            dataEntity.PublishDate = domainEntity.PublishDate;
        }

        public override SeedList ToDomainEntity(tblSeedList dataEntity)
        {
            var ret = new SeedList();
            ret.Name = dataEntity.Name;
            ret.PublishDate = dataEntity.PublishDate;
            foreach (var item in dataEntity.tblSeedListItem)
                ret.AddItem(TranslateItem(item));
            ret.Id = dataEntity.Id;
            return ret;
        }

        private SeedListItemBase TranslateItem(tblSeedListItem dataEntity)
        {
            SeedListItemBase poco;
            if (dataEntity.Type == (int)SeedListItemTranslator.SeedListItemType.CollectionSeedListItem)
            {
                poco = new CollectionSeedListItem(_collectionItemTranslator.ToDomainEntity(dataEntity.tblCollectionItem));
            }
            else if (dataEntity.Type == (int)SeedListItemTranslator.SeedListItemType.SupplierSeedListItem)
            {
                poco = new SupplierSeedListItem(dataEntity.Code, _taxonTranslator.ToDomainEntity(dataEntity.tblTaxon));
                FillSupplierSeedListItemPoco(poco as SupplierSeedListItem, dataEntity);
            }
            else
                throw new NotImplementedException(SeedListItemTranslator.GetNotImplementedMessage(dataEntity));
            poco.Id = dataEntity.Id;
            poco.SeedListId = dataEntity.tblSeedListId;
            poco.Pocket1000sPrice = dataEntity.Pocket1000sPrice;
            poco.Pocket100sPrice = dataEntity.Pocket100sPrice;
            poco.Pocket500sPrice = dataEntity.Pocket500sPrice;
            poco.StandardPocketCount = dataEntity.StandardPocketCount;
            poco.StandardPocketPrice = dataEntity.StandardPocketPrice;
            return poco;
        }

        private void FillSupplierSeedListItemPoco(SupplierSeedListItem poco, tblSeedListItem entity)
        {
            poco.Supplier = _supplierTranslator.ToDomainEntity(entity.tblSupplier);
            poco.SupplierCode = entity.SupplierCode;
        }
    }
}
