using CactusGuru.Domain.Greenhouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CactusGuru.Domain.Greenhouse.Formatting.Genera;

namespace CactusGuru.Domain.Test.Greenhouse
{
    [TestClass]
    public class SupplierTest
    {
        [TestMethod]
        public void Equals_WithNull_False()
        {
            Assert.IsFalse(new Supplier().Equals(null));
        }

        [TestMethod]
        public void Equals_WithNullObject_False()
        {
            Assert.IsFalse(new Supplier().Equals(Supplier.Empty));
        }

        [TestMethod]
        public void Equals_WithOtherType_False()
        {
            Assert.IsFalse(new Supplier().Equals(new Genus()));
        }

        [TestMethod]
        public void Equals_WithTheSameID_True()
        {
            var s1 = new Supplier {Id = Guid.NewGuid()};
            var s2 = new Supplier {Id = Guid.NewGuid()};
            Assert.IsFalse(s1.Equals(s2));

            s1.Id = s2.Id;
            Assert.IsTrue(s1.Equals(s2));
        }
    }
}
