using CactusGuru.Infrastructure.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace CactusGuru.Infrastructure.Test.Utils
{
    [TestClass]
    public class ArgumentCheckerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckNull_NullPassed_ThrowArgumentNullException()
        {
            ArgumentChecker.CheckNull(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckEmpty_WhenEntitiyHasEmptyId_ThrowArgumentException()
        {
            ArgumentChecker.CheckEmpty(new Mock<DomainEntity>().Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckEmpty_WhenIdIsEmpty_ThrowArgumentException()
        {
            ArgumentChecker.CheckEmpty(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckIs()
        {
            ArgumentChecker.CheckIs(12, typeof (string));
        }
    }
}
