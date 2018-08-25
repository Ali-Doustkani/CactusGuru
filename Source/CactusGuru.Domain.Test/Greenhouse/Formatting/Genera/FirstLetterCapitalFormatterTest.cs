using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Domain.Test.Greenhouse.Formatting
{
    [TestClass]
    public class FirstLetterCapitalFormatterTest
    {
        private GenusFirstLetterCapitalFormatter _formatter;

        [TestInitialize]
        public void SetUp()
        {
            _formatter = new GenusFirstLetterCapitalFormatter();
        }

        [TestMethod]
        public void Format_IfGeneraIsNull_EmptyString()
        {
            Assert.AreEqual(string.Empty, _formatter.Format(null));
        }

        [TestMethod]
        public void Format_IfGeneraNameIsEmpty_EmptyString()
        {
            var Genera = new Genus();
            Genera.Title = null;
            Assert.AreEqual(string.Empty, _formatter.Format(Genera));

            Genera.Title = string.Empty;
            Assert.AreEqual(string.Empty, _formatter.Format(Genera));
        }

        [TestMethod]
        public void Format()
        {
            var Genera = new Genus();
            Genera.Title = "ASTROPHYTUM";

            Assert.AreEqual("Astrophytum", _formatter.Format(Genera));
        }
    }
}
