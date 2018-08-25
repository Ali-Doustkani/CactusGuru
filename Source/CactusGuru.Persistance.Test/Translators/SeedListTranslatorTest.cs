using System;
using System.Linq;
using CactusGuru.Domain.Greenhouse.Sales;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Translators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Persistance.Test.Translators
{
    [TestClass]
    public class SeedListTranslatorTest
    {
        private SeedListTranslator _translator;

        [TestMethod]
        public void ToEntity()
        {
            var poco = new SeedList();

            var entity = _translator.ToDataEntity(poco);

            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public void FillEntity()
        {
            var poco = new SeedList();
            var entity = new tblSeedList();

            _translator.FillDataEntity(entity, poco);

            Assert.AreEqual(poco.Id, entity.Id);
            Assert.AreEqual(poco.Name, entity.Name);
            Assert.AreEqual(poco.PublishDate, entity.PublishDate);
        }

        [TestMethod]
        public void ToPoco()
        {
            var entity = new tblSeedList();
            entity.Id = Guid.NewGuid();
            entity.Name = "s1";
            entity.PublishDate = DateTime.Now;

            var poco = _translator.ToDomainEntity(entity);

            Assert.AreEqual(entity.Id, poco.Id);
            Assert.AreEqual(entity.Name, poco.Name);
            Assert.AreEqual(entity.PublishDate, poco.PublishDate);
        }

        [TestMethod]
        public void ToPoco_Items()
        {
            var entity = new tblSeedList();
            entity.tblSeedListItem.Add(new tblSeedListItem { Id = Guid.NewGuid(), Type = 1 });

            var poco = _translator.ToDomainEntity(entity);

            Assert.AreEqual(1, poco.Items.Count());
        }
    }
}
