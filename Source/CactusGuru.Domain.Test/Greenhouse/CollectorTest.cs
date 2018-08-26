using CactusGuru.Domain.Greenhouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CactusGuru.Domain.Test.Greenhouse
{
    [TestClass]
    public class CollectorTest
    {
        [TestMethod]
        public void Equals_WithNull_False()
        {
            Assert.IsFalse(new Collector().Equals(null));
        }

        [TestMethod]
        public void Equals_WithNullObject_False()
        {
            Assert.IsFalse(new Collector().Equals(Collector.Empty));
        }

        [TestMethod]
        public void Equals_WithOtherTypes_False()
        {
            Assert.IsFalse(new Collector().Equals(new Genus()));
        }

        [TestMethod]
        public void Equals_WithTheSameIDs_True()
        {
            var t1 = new Collector {Id = Guid.NewGuid()};
            var t2 = new Collector {Id = Guid.NewGuid()};
            Assert.IsFalse(t1.Equals(t2));

            t1.Id = t2.Id;
            Assert.IsTrue(t1.Equals(t2));
        }
    }
}
