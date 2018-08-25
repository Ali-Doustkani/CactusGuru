using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Translators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace CactusGuru.Persistance.Test.Translators
{
    [TestClass]
    public class TaxonTranslatorTest
    {
        private TaxonTranslator _translator;
        private Mock<TranslatorBase<Genus, tblGenus>> _genusTranslator;

        [TestInitialize]
        public void SetUp()
        {
            _genusTranslator = new Mock<TranslatorBase<Genus, tblGenus>>();
            _translator = new TaxonTranslator(_genusTranslator.Object);
        }

        [TestMethod]
        public void ToPoco_Test()
        {
            var entity = new tblTaxon();
            entity.Cultivar = "cultivar";
            entity.tblGenus = new tblGenus { Id = Guid.NewGuid() };
            entity.Id = Guid.NewGuid();
            entity.Species = "spec";
            entity.SubSpecies = "ssp";
            entity.Variety = "var";
            entity.Forma = "fa";

            var poco = _translator.ToDomainEntity(entity);

            Assert.AreEqual("cultivar", poco.Cultivar);
            Assert.AreEqual(entity.tblGenus.Id, poco.GeneraId);
            Assert.AreEqual(entity.Id, poco.Id);
            Assert.AreEqual("spec", poco.Species);
            Assert.AreEqual("ssp", poco.SubSpecies);
            Assert.AreEqual("var", poco.Variety);
            Assert.AreEqual("fa", poco.Forma);
        }

        [TestMethod]
        public void FillEntity_Test()
        {
            var entity = new tblTaxon();
            var poco = new Taxon { Id = Guid.NewGuid() };
            poco.Cultivar = "cultivar";
            var Genera = new Genus();
            Genera.Id = Guid.NewGuid();
            poco.Genus = Genera;
            poco.Species = "species";
            poco.SubSpecies = "sub";
            poco.Variety = "var";
            poco.Forma = "fa";

            _translator.FillDataEntity(entity, poco);

            Assert.AreEqual(poco.Id, entity.Id);
            Assert.AreEqual("cultivar", entity.Cultivar);
            Assert.AreEqual(poco.Genus.Id, entity.tblGenusId);
            Assert.AreEqual("species", entity.Species);
            Assert.AreEqual("sub", entity.SubSpecies);
            Assert.AreEqual("var", entity.Variety);
            Assert.AreEqual("fa", entity.Forma);
        }

        [TestMethod]
        public void Taxon_Properties_Count()
        {
            Assert.AreEqual(8, typeof(tblTaxon).GetProperties().Count());
        }
    }
}
