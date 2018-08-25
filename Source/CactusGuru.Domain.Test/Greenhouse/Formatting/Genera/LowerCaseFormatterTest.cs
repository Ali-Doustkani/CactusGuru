using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting.Genera;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Domain.Test.Greenhouse.Formatting.Genera
{
    [TestClass]
    public class LowerCaseFormatterTest
    {
        [TestMethod]
        public void FormatAllToLowerCases()
        {
            var Genera = new Genus();
            Genera.Title = "CINTIA";

            var formatter = new GenusLowerCaseFormatter( );
            Assert.AreEqual(formatter.Format(Genera), "cintia");
        }
    }
}
