using CactusGuru.Domain.Greenhouse;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Translators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CactusGuru.Persistance.Test.Translators
{
    [TestClass]
    public class SupplierTranslatorTest
    {
        private SupplierTranslator _translator;

        [TestInitialize]
        public void SetUp()
        {
            _translator = new SupplierTranslator();
        }

        [TestMethod]
        public void ToPoco()
        {
            var entity = new tblSupplier();
            entity.Acronym = "acron";
            entity.Title = "ali";
            entity.Id = Guid.NewGuid();
            entity.WebSite = "web";
            var poco = _translator.ToDomainEntity(entity);
            Assert.AreEqual("acron", poco.Acronym);
            Assert.AreEqual("ali", poco.FullName);
            Assert.AreEqual(entity.Id, poco.Id);
            Assert.AreEqual("web", poco.WebSite);
        }

        [TestMethod]
        public void ToPoco_IfEntityIsNull_ReturnEmpty()
        {
            Assert.AreEqual(_translator.ToDomainEntity(null), (Supplier.Empty));
        }

        [TestMethod]
        public void FillEntity()
        {
            var poco = new Supplier();
            poco.Acronym = "sb";
            poco.Id = Guid.NewGuid();
            poco.FullName = "steven brack";
            poco.WebSite = "web";
            var entity = new tblSupplier();

            _translator.FillDataEntity(entity, poco);

            Assert.AreEqual("sb", entity.Acronym);
            Assert.AreEqual(poco.Id, entity.Id);
            Assert.AreEqual("steven brack", entity.Title);
            Assert.AreEqual("web", entity.WebSite);
        }
    }
}
