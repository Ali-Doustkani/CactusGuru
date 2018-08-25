using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;
using System;

namespace CactusGuru.Persistance.Translators
{
    public class CollectionItemTranslator : TranslatorBase<CollectionItem, tblCollectionItem>
    {
        public CollectionItemTranslator(
            TranslatorBase<Collector, tblCollector> collectorTranslator,
            TranslatorBase<Supplier, tblSupplier> supplierTranslator,
            TranslatorBase<Taxon, tblTaxon> taxonTranslator)
        {
            ArgumentChecker.CheckNull(collectorTranslator);
            ArgumentChecker.CheckNull(supplierTranslator);
            ArgumentChecker.CheckNull(taxonTranslator);
            _collectorTranslator = collectorTranslator;
            _supplierTranslator = supplierTranslator;
            _taxonTranslator = taxonTranslator;
        }

        private readonly TranslatorBase<Collector, tblCollector> _collectorTranslator;
        private readonly TranslatorBase<Supplier, tblSupplier> _supplierTranslator;
        private readonly TranslatorBase<Taxon, tblTaxon> _taxonTranslator;

        public override void FillDataEntity(tblCollectionItem dataEntity, CollectionItem domainEntity)
        {
            dataEntity.Code = domainEntity.Code;
            dataEntity.tblCollectorId = GetCollectorId(domainEntity);
            dataEntity.Count = domainEntity.Count;
            dataEntity.Description = domainEntity.Description;
            dataEntity.FieldNumber = domainEntity.FieldNumber;
            dataEntity.Id = domainEntity.Id;
            dataEntity.IncomeType = (byte)domainEntity.IncomeType;
            dataEntity.Locality = domainEntity.Locality;
            dataEntity.tblSupplierId = GetSupplierId(domainEntity);
            dataEntity.tblTaxonId = domainEntity.Taxon.Id;
            dataEntity.SupplierCode = domainEntity.SupplierCode;
            dataEntity.IncomeDate = domainEntity.IncomeDate;
        }

        public override CollectionItem ToDomainEntity(tblCollectionItem entity)
        {
            var ret = new CollectionItem();
            ret.Code = entity.Code;
            ret.Collector = _collectorTranslator. ToDomainEntity(entity.tblCollector);
            ret.Count = entity.Count;
            ret.Description = entity.Description;
            ret.FieldNumber = entity.FieldNumber;
            ret.Id = entity.Id;
            ret.IncomeDate = entity.IncomeDate;
            ret.IncomeType = (IncomeType)entity.IncomeType;
            ret.Locality = entity.Locality;
            ret.Supplier = _supplierTranslator. ToDomainEntity(entity.tblSupplier);
            ret.Taxon = _taxonTranslator.ToDomainEntity(entity.tblTaxon);
            ret.SupplierCode = entity.SupplierCode;
            return ret;
        }

        private Guid? GetSupplierId(CollectionItem poco)
        {
            if (poco.Supplier == Supplier.Empty)
                return null;
            return poco.Supplier.Id;
        }

        private Guid? GetCollectorId(CollectionItem poco)
        {
            if (poco.Collector == Collector.Empty)
                return null;
            return poco.Collector.Id;
        }
    }
}
