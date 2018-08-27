using CactusGuru.Infrastructure.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Infrastructure.Test.Utils
{
    [TestClass]
    public class StringFormattingTest
    {
        [TestMethod]
        public void Format1()
        {
            AssertFormat("1 - 2", "{a} - {b}");
        }

        [TestMethod]
        public void Format2()
        {
            AssertFormat("1, 2", "{a}{, b}");
        }

        [TestMethod]
        public void Format3()
        {
            AssertFormat("1 , 2", "{a} , {b}{, c}");
        }

        private void AssertFormat(string expected, string format)
        {
            var result = StringFormatting.Tokenize(format, token =>
            {
                if (token == "a")
                    return "1";
                else if (token == "b")
                    return "2";
                return string.Empty;
            });

            Assert.AreEqual(expected, result);
        }
    }
}
