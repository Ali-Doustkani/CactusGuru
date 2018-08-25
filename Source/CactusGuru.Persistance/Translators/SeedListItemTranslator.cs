using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Sales;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;
using System;

namespace CactusGuru.Persistance.Translators
{
    public class SeedListItemTranslator : TranslatorBase<SeedListItemBase, tblSeedListItem>
    {
        public SeedListItemTranslator(TranslatorBase<Supplier, tblSupplier> supplierTranslator)
        {
            ArgumentChecker.CheckNull(supplierTranslator);
            _supplierTranslator = supplierTranslator;
        }

        private TranslatorBase<Supplier, tblSupplier> _supplierTranslator;

        public override void FillDataEntity(tblSeedListItem dataEntity, SeedListItemBase domainEntity)
        {
            dataEntity.tblSeedListId = domainEntity.SeedListId;
            dataEntity.Code = domainEntity.Code;
            dataEntity.Id = domainEntity.Id;
            dataEntity.Pocket1000sPrice = domainEntity.Pocket1000sPrice;
            dataEntity.Pocket100sPrice = domainEntity.Pocket100sPrice;
            dataEntity.Pocket500sPrice = domainEntity.Pocket500sPrice;
            dataEntity.StandardPocketCount = domainEntity.StandardPocketCount;
            dataEntity.StandardPocketPrice = domainEntity.StandardPocketPrice;

            if (domainEntity is CollectionSeedListItem)
                FillCollectionSeedListItemEntity(dataEntity, domainEntity as CollectionSeedListItem);
            else if (domainEntity is SupplierSeedListItem)
                FillSupplierSeedListItemEntity(dataEntity, domainEntity as SupplierSeedListItem);
            else
                throw new NotImplementedException(GetNotImplementedMessage(domainEntity));
        }

        private void FillCollectionSeedListItemEntity(tblSeedListItem entity, CollectionSeedListItem poco)
        {
            entity.Type = (int)SeedListItemType.CollectionSeedListItem;
            entity.tblCollectionItemId = poco.CollectionItem.Id;
        }

        private void FillSupplierSeedListItemEntity(tblSeedListItem entity, SupplierSeedListItem poco)
        {
            entity.Type = (int)SeedListItemType.SupplierSeedListItem;
            entity.SupplierCode = poco.SupplierCode;
            entity.tblSupplierId = poco.Supplier.Id;
            entity.tblTaxonId = poco.Taxon.Id;
        }

        public static string GetNotImplementedMessage(object obj)
        {
            return string.Format("{0} translation is not implemented yet.", obj.GetType().Name);
        }

        public override SeedListItemBase ToDomainEntity(tblSeedListItem dataEntity)
        {
            throw new NotImplementedException();
        }

        public enum SeedListItemType
        {
            CollectionSeedListItem = 1,
            SupplierSeedListItem = 2
        }
    }
}
