using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Domain.Test.Greenhouse.Formatting
{
    [TestClass]
    public class AcronymFormatterTest
    {
        [TestMethod]
        public void FormatCollectorWithFieldInParantheses()
        {
            var col = new Collector { FullName = "Steven Brack", Acronym = "SB" };
            var formatter = new CollectorFormatter( );
            Assert.AreEqual(formatter.Format(col), "Steven Brack (SB)");
        }

        [TestMethod]
        public void FormatCollectorWithoutFieldWithNoParantheses()
        {
            var col = new Collector { FullName = "Pavel Pavlicek" };
            var formatter = new CollectorFormatter( );
            Assert.AreEqual(formatter.Format(col), "Pavel Pavlicek");
        }

        [TestMethod]
        public void FormatFieldWithoutCollectorWithNoParantheses()
        {
            var col = new Collector {FullName = string.Empty, Acronym = "HO"};
            var formatter = new CollectorFormatter();

            Assert.AreEqual("HO", formatter.Format(col));
        }

        [TestMethod]
        public void WhenTitleAndAcronymIsNull_ReturnEmptyString()
        {
            var supplier = new Collector();
            supplier.FullName = null;
            supplier.Acronym = null;
            var formatter = new CollectorFormatter();

            Assert.AreEqual(string.Empty, formatter.Format(supplier));
        }
    }
}
