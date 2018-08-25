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
        private Mock<IFormatter<Taxon>> _taxonFormatter;

        [TestInitialize]
        public void SetUp()
        {
            _taxonFormatter = new Mock<IFormatter<Taxon>>();
            _formatter = new CollectionItemFormatter(_taxonFormatter.Object);
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
            var Genera = new Mock<Genus>(new GenusCapitalFormatter());
            Genera.Setup(x => x.Title).Returns("astrophytum");
            var taxon = new Mock<Taxon>();
            taxon.Setup(x => x.Genus).Returns(Genera.Object);
            taxon.Setup(x => x.Species).Returns("asterias");
            var item = new Mock<CollectionItem>();
            item.Setup(x => x.Taxon).Returns(taxon.Object);

            Assert.AreEqual(_formatter.Format(item.Object), "ASTROPHYTUM asterias");
        }

        [TestMethod]
        public void FormatComplete()
        {
            var Genera = new Mock<Genus>(new GenusCapitalFormatter());
            Genera.Setup(x => x.Title).Returns("astrophytum");
            var taxon = new Mock<Taxon>();
            taxon.Setup(x => x.Genus).Returns(Genera.Object);
            taxon.Setup(x => x.Species).Returns("asterias");
            var collector = new Mock<Collector>();
            collector.Setup(x => x.Acronym).Returns("L");
            var item = new Mock<CollectionItem>();
            item.Setup(x => x.Taxon).Returns(taxon.Object);
            item.Setup(x => x.Collector).Returns(collector.Object);
            item.Setup(x => x.FieldNumber).Returns("80");

            Assert.AreEqual(_formatter.Format(item.Object), "ASTROPHYTUM asterias L80");
        }
    }
}
