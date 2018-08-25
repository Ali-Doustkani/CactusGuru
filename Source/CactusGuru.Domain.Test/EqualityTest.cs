using System;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting.Genera;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Domain.Test
{
    [TestClass]
    public class EqualityTest
    {
        [TestMethod]
        public void Is_Not_Equal_With_Other_Types()
        {
            var t1 = new Genus();
            var t2 = new Taxon();
            Assert.IsFalse(t1.Equals(t2));
        }

        [TestMethod]
        public void Is_Equal_With_Same_Reference()
        {
            var t1 = new Genus();
            Assert.IsTrue(t1.Equals(t1));
        }

        [TestMethod]
        public void Different_Entities_With_The_Same_Type_Are_Equal_Based_On_Id()
        {
            var id = Guid.NewGuid();
            var t1 =    new Genus(){ Id = id };
            var t2 = new Genus(){ Id = id };
            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod]
        public void Different_Entities_With_Same_Type_And_Different_Ids_Are_Not_Equal()
        {
            var t1 = new Genus( ){ Id = Guid.NewGuid() };
            var t2 = new Genus(){ Id = Guid.NewGuid() };
            Assert.IsFalse(t1.Equals(t2));
        }
    }
}
