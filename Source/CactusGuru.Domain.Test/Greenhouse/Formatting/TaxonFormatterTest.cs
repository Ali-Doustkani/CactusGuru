using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Domain.Test.Greenhouse.Formatting
{
    [TestClass]
    public class TaxonFormatterTest
    {
        private TaxonFormatter _formatter;
        private Taxon _taxon;

        [TestInitialize]
        public void Setup()
        {
            var genusFormatter = new GenusCapitalFormatter();
            _formatter = new TaxonFormatter(genusFormatter);
            _taxon = new Taxon();
            _taxon.Genus = new Genus();
        }

        private void Genus(string name)
        {
            _taxon.Genus.Title = name;
        }

        private void Species(string sp)
        {
            _taxon.Species = sp;
        }

        private void Cultivar(string cv)
        {
            _taxon.Cultivar = cv;
        }

        private string Format()
        {
            return _formatter.Format(_taxon);
        }

        [TestMethod]
        public void Test1()
        {
            Genus("astrophytum");
            Species("asterias");
            Cultivar("superkabatu");

            var result = Format();

            Assert.AreEqual("ASTROPHYTUM asterias cv. Superkabatu", result);
        }

        [TestMethod]
        public void Test2()
        {
            Genus("astrophytum");
            Species("Asterias");

            var result = Format();

            Assert.AreEqual("ASTROPHYTUM asterias", result);
        }
    }
}
