using CactusGuru.Domain.Greenhouse;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Translators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CactusGuru.Persistance.Test.Translators
{
    [TestClass]
    public class CollectorTranslatorTest
    {
        private CollectorTranslator _translator;

        [TestInitialize]
        public void SetUp()
        {
            _translator = new CollectorTranslator();
        }

        [TestMethod]
        public void ToPoco()
        {
            var entity = new tblCollector();
            entity.FieldAcronym = "field";
            entity.Title = "first";
            entity.Id = Guid.NewGuid();
            entity.WebSite = "web";

            var poco = _translator.ToDomainEntity(entity);

            Assert.AreEqual("field", poco.Acronym);
            Assert.AreEqual("first", poco.FullName);
            Assert.AreEqual(entity.Id, poco.Id);
            Assert.AreEqual("web", poco.WebSite);
        }

        [TestMethod]
        public void ToPoco_IfEntityIsNull_ReturnEmpty()
        {
            Assert.AreEqual(_translator.ToDomainEntity(null), (Collector.Empty));
        }

        [TestMethod]
        public void FillEntity()
        {
            var entity = new tblCollector();
            var poco = new Collector();
            poco.Acronym = "pp";
            poco.Id = Guid.NewGuid();
            poco.FullName = "pavel";
            poco.WebSite = "cact.cz";

            _translator.FillDataEntity(entity, poco);

            Assert.AreEqual("pp", entity.FieldAcronym);
            Assert.AreEqual(poco.Id, entity.Id);
            Assert.AreEqual("pavel", entity.Title);
            Assert.AreEqual("cact.cz", entity.WebSite);
        }
    }
}
