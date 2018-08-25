using System;
using System.Collections.Generic;
using CactusGuru.Domain.Greenhouse.Qualification.Inquiries;
using CactusGuru.Infrastructure.Persistance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CactusGuru.Domain.Test.Greenhouse.Qualification.Inquiries
{
    [TestClass]
    public class InquiryBaseTest
    {
        private InquiryTest _inq;

        [TestInitialize]
        public void SetUp()
        {
            _inq = new InquiryTest();
            _inq.UnitOfWork = new Mock<IUnitOfWork>().Object;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Inquiry_IfUnitOfWorkIsNull_ThrowInvalidOperationException()
        {
            _inq.UnitOfWork = null;
            _inq.Inquiry();
        }

        [TestMethod]
        public void InquiryMessage()
        {
            Assert.IsTrue(_inq.InquiryMessage().Contains("a"));
            Assert.IsTrue(_inq.InquiryMessage().Contains("b"));
        }

        private class InquiryTest : InquiryBase
        {
            public override IUnitOfWork UnitOfWork { get; set; }

            protected override IEnumerable<string> InquiryImp()
            {
                return new List<string> { "a", "b" };
            }
        }
    }
}
