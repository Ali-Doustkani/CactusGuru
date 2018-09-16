using System;
using CactusGuru.Infrastructure.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Presentation.ViewModel.Test.Tools
{
    [TestClass]
    public class DateUtilTest
    {
        [TestMethod]
        public void ToPersianDate()
        {
            var persianDate = DateUtil.ToPersianDate(new DateTime(2015, 12, 29));

            Assert.AreEqual("1394/10/8", persianDate);
        }

        [TestMethod]
        public void FromPersianDate()
        {
            var date= DateUtil.FromPersianDate("1394/10/8");

            Assert.AreEqual(2015, date.Year);
            Assert.AreEqual(12, date.Month);
            Assert.AreEqual(29, date.Day);
        }

        [TestMethod]
        public void IsValid()
        {
            Assert.IsFalse(DateUtil.IsValid(""));
        }
    }
}
