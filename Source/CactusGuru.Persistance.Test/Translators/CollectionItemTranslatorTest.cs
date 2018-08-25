using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Translators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CactusGuru.Persistance.Test.Translators
{
    [TestClass]
    public class CollectionItemTranslatorTest
    {
        private CollectionItemTranslator _translator;
        private Mock<TranslatorBase<Collector, tblCollector>> _collectorTranslator;
        private Mock<TranslatorBase<Supplier, tblSupplier>> _supplierTranslator;
        private Mock<TranslatorBase<Taxon, tblTaxon>> _taxonTranslator;

        [TestInitialize]
        public void SetUp()
        {
            _collectorTranslator = new Mock<TranslatorBase<Collector, tblCollector>>();
            _supplierTranslator = new Mock<TranslatorBase<Supplier, tblSupplier>>();
            _taxonTranslator = new Mock<TranslatorBase<Taxon, tblTaxon>>();
            _translator = new CollectionItemTranslator(_collectorTranslator.Object, _supplierTranslator.Object,
                _taxonTranslator.Object);
        }

        [TestMethod]
        public void FillEntity()
        {
            var poco = new CollectionItem();
            poco.Code = "123";
            var collector = new Collector();
            collector.Id = Guid.NewGuid();
            poco.Collector = collector;
            poco.Count = 13;
            poco.Description = "desc";
            poco.FieldNumber = "FF";
            poco.Id = Guid.NewGuid();
            poco.IncomeDate = new DateTime(2111, 2, 3);
            poco.IncomeType = IncomeType.Seed;
            poco.Locality = "mexico";
            var supp = new Supplier();
            supp.Id = Guid.NewGuid();
            poco.Supplier = supp;
            poco.Taxon = new Taxon { Id = Guid.NewGuid() };
            poco.SupplierCode = "12.36";

            var entity = new tblCollectionItem();
            _translator.FillDataEntity(entity, poco);

            Assert.AreEqual(entity.Code, ("123"));
            Assert.AreEqual(entity.tblCollectorId, (poco.Collector.Id));
            Assert.AreEqual(entity.Count, (13));
            Assert.AreEqual(entity.Description, ("desc"));
            Assert.AreEqual(entity.FieldNumber, ("FF"));
            Assert.AreEqual(entity.Id, (poco.Id));
            Assert.AreEqual(entity.IncomeDate, (new DateTime(2111, 2, 3)));
            Assert.AreEqual(entity.IncomeType, (1));
            Assert.AreEqual(entity.Locality, ("mexico"));
            Assert.AreEqual(entity.tblSupplierId, (poco.Supplier.Id));
            Assert.AreEqual(entity.tblTaxonId, (poco.Taxon.Id));
            Assert.AreEqual(entity.SupplierCode, (poco.SupplierCode));
        }

        [TestMethod]
        public void ToPoco()
        {
            var entity = CreateEntity();
            var poco = _translator.ToDomainEntity(entity);
            Assert.AreEqual(poco.Code, (entity.Code));
            Assert.AreEqual(poco.Count, (33));
            Assert.AreEqual(poco.Description, ("desc"));
            Assert.AreEqual(poco.FieldNumber, ("ff"));
            Assert.AreEqual(poco.Id, (entity.Id));
            Assert.AreEqual(poco.IncomeDate, (new DateTime(2015, 1, 2)));
            Assert.AreEqual(poco.IncomeType, (IncomeType.Plant));
            Assert.AreEqual(poco.Locality, ("meix"));
            Assert.AreEqual(poco.SupplierCode, entity.SupplierCode);
        }

        [TestMethod]
        public void ToEntity_IfCollectorIsEmptyReturnNull()
        {
            var poco = new CollectionItem();
            poco.Collector = Collector.Empty;
            var entity = _translator.ToDataEntity(poco);
            Assert.IsNull(entity.tblCollectorId);
        }

        [TestMethod]
        public void ToEntity_IfSupplierIsEmptyReturnNull()
        {
            var poco = new CollectionItem();
            poco.Supplier = Supplier.Empty;
            var entity = _translator.ToDataEntity(poco);
            Assert.IsNull(entity.tblSupplierId);
        }

        private tblCollectionItem CreateEntity()
        {
            var entity = new tblCollectionItem();
            entity.Code = "11";
            entity.tblCollector = new tblCollector { Id = Guid.NewGuid() };
            entity.Count = 33;
            entity.Description = "desc";
            entity.FieldNumber = "ff";
            entity.Id = Guid.NewGuid();
            entity.IncomeDate = new DateTime(2015, 1, 2);
            entity.IncomeType = 2;
            entity.Locality = "meix";
            entity.tblSupplier = new tblSupplier { Id = Guid.NewGuid() };
            entity.tblTaxon = new tblTaxon { Id = Guid.NewGuid() };
            entity.tblTaxon.tblGenus = new tblGenus();
            return entity;
        }
    }
}
