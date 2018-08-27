using CactusGuru.Domain.Greenhouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CactusGuru.Domain.Test.Greenhouse
{
    [TestClass]
    public class GenusTest
    {
        [TestMethod]
        public void CapitalLetters()
        {
            Assert.AreEqual("ASTRO", Genus().Format("GENUS"));
        }

        [TestMethod]
        public void FirstLetterCapitalized()
        {
            Assert.AreEqual("Astro", Genus().Format("Genus"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void InvalidFormat_Exception()
        {
            Genus().Format("GEnus");
        }

        private Genus Genus()
        {
            return new Genus { Title = "astRo" };
        }
    }
}
