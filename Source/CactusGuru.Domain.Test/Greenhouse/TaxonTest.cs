using CactusGuru.Domain.Greenhouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CactusGuru.Domain.Test.Greenhouse
{
    [TestClass]
    public class TaxonTest
    {
        [TestMethod]
        public void NewTaxonHasNullGenera()
        {
            var taxon = new Taxon();
            Assert.AreEqual(taxon.Genus, Genus.Empty);
        }

        [TestMethod]
        public void NullTaxonMustHaveEmptyProperties()
        {
            Assert.AreEqual(Taxon.Empty.Cultivar, string.Empty);
            Assert.AreEqual(Taxon.Empty.ToString(), "EMPTY TAXON");
            Assert.AreEqual(Taxon.Empty.Genus, Genus.Empty);
            Assert.AreEqual(Taxon.Empty.Id, Guid.Empty);
            Assert.AreEqual(Taxon.Empty.Species, string.Empty);
            Assert.AreEqual(Taxon.Empty.SubSpecies, string.Empty);
            Assert.AreEqual(Taxon.Empty.Variety, string.Empty);
        }

        [TestMethod]
        public void CanSetGenera()
        {
            var GeneraId = Guid.NewGuid();
            var taxon = new Taxon { Genus = new Genus() { Id = GeneraId } };
            Assert.AreEqual(taxon.Genus.Id, GeneraId);
            var Genera = new Genus();
            Genera.Title = "astro";
            (taxon as Taxon).Genus = Genera;
            Assert.AreEqual((taxon as Taxon).Genus.Title, "astro");
        }

        [TestMethod]
        public void Equals_WithNull_False()
        {
            Assert.IsFalse(new Taxon().Equals(null));
        }

        [TestMethod]
        public void Equals_WithNullObject_False()
        {
            Assert.IsFalse(new Taxon().Equals(Taxon.Empty));
        }

        [TestMethod]
        public void Equals_WithOtherTypes_False()
        {
            Assert.IsFalse(new Taxon().Equals(new Genus()));
        }

        [TestMethod]
        public void Equals_IfIDsAreTheSame_True()
        {
            var t1 = new Taxon { Id = Guid.NewGuid() };
            var t2 = new Taxon { Id = Guid.NewGuid() };
            Assert.IsFalse(t1.Equals(t2));

            t2.Id = t1.Id;
            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod]
        public void FormatGenusAndTaxon()
        {
            Assert.AreEqual("LOBIVIA ferox", Lobivia().ToString("{GENUS} {taxon}"));
        }

        [TestMethod]
        public void FormatOnlyGenus()
        {
            Assert.AreEqual("Lobivia", Lobivia().ToString("{Genus}"));
        }

        [TestMethod]
        public void FormatOnlyTaxon()
        {
            Assert.AreEqual("ferox", Lobivia().ToString("{taxon}"));
        }

        [TestMethod]
        public void FormatCultivatedTaxon()
        {
            Assert.AreEqual("ASTROPHYTUM asterias cv. Superkabuto", Superkubato().ToString("{GENUS} {taxon}"));
        }

        private Taxon Lobivia()
        {
            return new Taxon
            {
                Genus = new Genus { Title = "lobivia" },
                Species = "ferox"
            };
        }

        private Taxon Superkubato()
        {
            return new Taxon
            {
                Genus = new Genus { Title = "astrophytum" },
                Species = "asterias",
                Cultivar = "superkabuto"
            };
        }
    }
}
