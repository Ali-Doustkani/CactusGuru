using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Translators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CactusGuru.Persistance.Test.Translators
{
    [TestClass]
    public class GenusTranslatorTest
    {
        private GenusTranslator _translator;
        private Mock<IFactory<Genus>> _factory;

        [TestInitialize]
        public void SetUp()
        {
            _factory = new Mock<IFactory<Genus>>();
            _factory.Setup(x => x.CreateNew()).Returns(new Genus());
            _translator = new GenusTranslator(_factory.Object);
        }

        [TestMethod]
        public void ToEntity()
        {
            var poco = new Genus();
            poco.Id = Guid.NewGuid();
            poco.Title = "gymno";

            var entity = _translator.ToDataEntity(poco);

            Assert.AreEqual(entity.Id, (poco.Id));
            Assert.AreEqual(entity.Name, ("gymno"));
        }

        [TestMethod]
        public void ToPoco()
        {
            var entity = new tblGenus { Id = Guid.NewGuid(), Name = "astro" };

            var poco = _translator.ToDomainEntity(entity);

            Assert.AreEqual(entity.Id, poco.Id);
            Assert.AreEqual("astro", poco.Title);
        }
    }
}
