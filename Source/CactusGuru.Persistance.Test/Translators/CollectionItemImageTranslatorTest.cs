using CactusGuru.Domain.Greenhouse;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Translators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CactusGuru.Persistance.Test.Translators
{
    [TestClass]
    public class CollectionItemImageTranslatorTest
    {
        private CollectionItemImageTranslator _translator;

        [TestInitialize]
        public void SetUp()
        {
            _translator = new CollectionItemImageTranslator();
        }

        [TestMethod]
        public void FillEntity()
        {
            var poco = new CollectionItemImage();
            poco.Id = Guid.NewGuid();
            poco.DateAdded = new DateTime(2015, 1, 2);
            poco.Description = "desc";
            poco.CollectionItemId = Guid.NewGuid();
            var entity = new tblCollectionItemImage();

            _translator.FillDataEntity(entity, poco);

            Assert.AreEqual(entity.Id, poco.Id);
            Assert.AreEqual(entity.tblCollectionItemId, poco.CollectionItemId);
            Assert.AreEqual(entity.DateAdded, new DateTime(2015, 1, 2));
            Assert.AreEqual(entity.Description, "desc");
        }

        [TestMethod]
        public void ToPoco()
        {
            var entity = new tblCollectionItemImage();
            entity.DateAdded = new DateTime(2014, 4, 5);
            entity.Description = "desc";
            entity.Id = Guid.NewGuid();
            entity.Image = new byte[] { 2, 3, 4 };

            var poco = _translator.ToDomainEntity(entity);

            Assert.AreEqual(poco.DateAdded, new DateTime(2014, 4, 5));
            Assert.AreEqual(poco.Description, "desc");
            Assert.AreEqual(poco.Id, entity.Id);
        }
    }
}
