using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Domain.Test.Greenhouse.Formatting
{
    [TestClass]
    public class CapitalGeneraFormatterTest
    {
        private GenusCapitalFormatter _formatter;

        [TestInitialize]
        public void SetUp()
        {
            _formatter = new GenusCapitalFormatter();
        }

        [TestMethod]
        public void Format_WhenGeneraIsNull_EmptyString()
        {
            Assert.AreEqual(string.Empty, _formatter.Format(null));
        }

        [TestMethod]
        public void Format_WhenNameIsNullOrEmpty_EmptyString()
        {
            var Genera = new Genus();
            Genera.Title = null;

            Assert.AreEqual(string.Empty, _formatter.Format(Genera));
        }

        [TestMethod]
        public void Format()
        {
            var Genera = new Genus();
            Genera.Title = "astro";

            Assert.AreEqual("ASTRO", _formatter.Format(Genera));
        }
    }
}
