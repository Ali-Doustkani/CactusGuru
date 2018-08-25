using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Sales;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Translators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CactusGuru.Persistance.Test.Translators
{
    [TestClass]
    public class SeedListItemTranslatorTest
    {
        private SeedListItemTranslator _translator;
        private Mock<TranslatorBase<Supplier, tblSupplier>> _supplierTranslator;

        [TestInitialize]
        public void SetUp()
        {
            _supplierTranslator = new Mock<TranslatorBase<Supplier, tblSupplier>>();
            _translator = new SeedListItemTranslator(_supplierTranslator.Object);
        }

        [TestMethod]
        public void FillEntity_GeneralData()
        {
            var poco = new SupplierSeedListItem("code", new Taxon { Id = Guid.NewGuid() });
            poco.Id = Guid.NewGuid();
            poco.Pocket1000sPrice = 1000;
            poco.Pocket100sPrice = 100;
            poco.Pocket500sPrice = 500;
            poco.StandardPocketCount = 20;
            poco.StandardPocketPrice = 2500;
            var entity = new tblSeedListItem();

            _translator.FillDataEntity(entity, poco);

            Assert.AreEqual(poco.Id, entity.Id);
            Assert.AreEqual("code", entity.Code);
            Assert.AreEqual(1000, entity.Pocket1000sPrice);
            Assert.AreEqual(100, entity.Pocket100sPrice);
            Assert.AreEqual(500, entity.Pocket500sPrice);
            Assert.AreEqual(20, entity.StandardPocketCount);
            Assert.AreEqual(2500, entity.StandardPocketPrice);
        }

        [TestMethod]
        public void FillEntity_When_Type_Is_CollectionSeedListItem()
        {
            var item = new CollectionItem();
            item.Id = new Guid();
            item.Code = "1";
            var poco = new CollectionSeedListItem(item);
            var entity = new tblSeedListItem();
            entity.tblSupplierId = Guid.NewGuid();
            entity.tblTaxonId = Guid.NewGuid();
            entity.SupplierCode = "supCode";

            _translator.FillDataEntity(entity, poco);

            Assert.AreEqual((int)SeedListItemTranslator.SeedListItemType.CollectionSeedListItem, entity.Type);
            Assert.AreEqual(item.Id, entity.tblCollectionItemId);
            Assert.IsNull(entity.tblSupplierId);
            Assert.IsNull(entity.tblTaxonId);
            Assert.IsNull(entity.SupplierCode);
        }

        [TestMethod]
        public void FillEntity_When_Type_Is_SupplierSeedListItem()
        {
            var taxon = new Taxon { Id = Guid.NewGuid() };
            var poco = new SupplierSeedListItem("code", taxon);
            poco.Supplier = new Supplier();
            poco.Supplier.Id = Guid.NewGuid();
            poco.SupplierCode = "supCode";
            var entity = new tblSeedListItem();
            entity.tblCollectionItemId = Guid.NewGuid();

            _translator.FillDataEntity(entity, poco);

            Assert.AreEqual((int)SeedListItemTranslator.SeedListItemType.SupplierSeedListItem, entity.Type);
            Assert.IsNull(entity.tblCollectionItemId);
            Assert.AreEqual(poco.Supplier.Id, entity.tblSupplierId);
            Assert.AreEqual("supCode", entity.SupplierCode);
            Assert.AreEqual(poco.Taxon.Id, entity.tblTaxonId);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void FillEntity_When_Type_Is_Invalid()
        {
            var poco = new Mock<SeedListItemBase>("code").Object;
            var entity = new tblSeedListItem();

            _translator.FillDataEntity(entity, poco);
        }

        [TestMethod]
        public void ToPoco_GeneralData()
        {
            var entity = new tblSeedListItem();
            entity.Type = (int)SeedListItemTranslator.SeedListItemType.CollectionSeedListItem;
            entity.Code = "code";
            entity.Id = Guid.NewGuid();
            entity.Pocket1000sPrice = 1000;
            entity.Pocket100sPrice = 100;
            entity.Pocket500sPrice = 500;
            entity.StandardPocketCount = 20;
            entity.StandardPocketPrice = 2800;

            var poco = _translator.ToDomainEntity(entity);

            Assert.AreEqual("code", poco.Code);
            Assert.AreEqual(entity.Id, poco.Id);
            Assert.AreEqual(1000, poco.Pocket1000sPrice);
            Assert.AreEqual(100, poco.Pocket100sPrice);
            Assert.AreEqual(500, poco.Pocket500sPrice);
            Assert.AreEqual(20, poco.StandardPocketCount);
            Assert.AreEqual(2800, poco.StandardPocketPrice);
        }

        [TestMethod]
        public void ToPoco_When_Type_Is_CollectionSeedListItem()
        {
            var entity = new tblSeedListItem();
            entity.Type = (int)SeedListItemType.CollectionItem;
            entity.tblCollectionItem = new tblCollectionItem();
            entity.tblCollectionItem.Id = Guid.NewGuid();

            var poco = _translator.ToDomainEntity(entity) as CollectionSeedListItem;

            Assert.AreEqual(entity.tblCollectionItem.Id, poco.CollectionItem.Id);
        }

        [TestMethod]
        public void ToPoco_When_Type_Is_SupplierSeedListItem()
        {
            var entity = new tblSeedListItem();
            entity.Type = (int)SeedListItemType.SupplierItem;
            entity.tblSupplier = new tblSupplier { Id = Guid.NewGuid() };
            entity.SupplierCode = "supCode";
            entity.tblTaxon = new tblTaxon { Id = Guid.NewGuid() };

            var poco = _translator.ToDomainEntity(entity) as SupplierSeedListItem;

            Assert.AreEqual(entity.tblSupplier.Id, poco.Supplier.Id);
            Assert.AreEqual(entity.SupplierCode, poco.SupplierCode);
            Assert.AreEqual(entity.tblTaxon.Id, poco.Taxon.Id);
        }
    }
}
