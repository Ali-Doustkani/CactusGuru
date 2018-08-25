using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Greenhouse.Formatting.CollectionItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CactusGuru.Domain.Test.Greenhouse.Formatting.CollectionItems
{
    [TestClass]
    public class CollectionItemFormatterTest
    {
        private CollectionItemFormatter _formatter;

        [TestInitialize]
        public void SetUp()
        {
            var taxonFormatter = new TaxonFormatter(new GenusCapitalFormatter());
            _formatter = new CollectionItemFormatter(taxonFormatter);
        }

        [TestMethod]
        public void FormatWithNullTaxon()
        {
            var item = new Mock<CollectionItem>();
            Assert.IsTrue(string.IsNullOrEmpty(_formatter.Format(item.Object)));
        }

        [TestMethod]
        public void FormatWithTaxon()
        {
            var taxon = new Taxon();
            taxon.Genus = new Genus { Title = "astrophytum" };
            taxon.Species = "asterias";
            var item = new CollectionItem();
            item.Taxon = taxon;

            Assert.AreEqual(_formatter.Format(item), "ASTROPHYTUM asterias");
        }

        [TestMethod]
        public void FormatComplete()
        {
            var taxon = new Taxon();
            taxon.Genus = new Genus { Title = "astrophytum" };
            taxon.Species = "asterias";
            var collector = new Collector();
            collector.Acronym = "L";
            var item = new CollectionItem();
            item.Taxon = taxon;
            item.Collector = collector;
            item.FieldNumber = "80";

            Assert.AreEqual(_formatter.Format(item), "ASTROPHYTUM asterias L80");
        }
    }
}
