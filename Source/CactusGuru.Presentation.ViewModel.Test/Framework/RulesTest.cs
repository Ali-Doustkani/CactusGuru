using CactusGuru.Presentation.ViewModel.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Test.Framework
{
    [TestClass]
    public class RulesTest
    {
        private Rules rules;

        [TestInitialize]
        public void Setup()
        {
            rules = new Rules(x => { });
        }

        [TestMethod]
        public void IsNotEmpty_WhenItsNull()
        {
            rules.MakeSure("sp").IsNotEmpty();

            rules.Check("sp", null);

            Assert.IsTrue(rules.AnyError());
            Assert.AreEqual(Rules.EMPTY_ERROR, rules.GetErrors("sp").First());
        }

        [TestMethod]
        public void IsNotEmpty_WhenItsEmpty()
        {
            rules.MakeSure("sp").IsNotEmpty();

            rules.Check("sp", string.Empty);

            Assert.IsTrue(rules.AnyError());
            Assert.AreEqual(Rules.EMPTY_ERROR, rules.GetErrors("sp").First());
        }

        [TestMethod]
        public void IsNotEmpty_WhenItsNotEmpty()
        {
            rules.MakeSure("sp").IsNotEmpty();

            rules.Check("sp", "OK");

            Assert.IsFalse(rules.AnyError());
        }

        [TestMethod]
        public void Validates_WhenItsValid()
        {
            rules.MakeSure("sp").ValidatesForWhole(() => null);

            rules.Check("sp", "correct value");

            Assert.IsFalse(rules.AnyError());
        }

        [TestMethod]
        public void Validates_WhenItsNotValid()
        {
            rules.MakeSure("sp").ValidatesForWhole(() => "CUSTOM_ERROR");

            rules.Check("sp", "failure");

            Assert.IsTrue(rules.AnyError());
            Assert.AreEqual("CUSTOM_ERROR", rules.GetErrors("").First());
        }

        [TestMethod]
        public void CheckRuleOrdersAscending()
        {
            rules.MakeSure("sp").IsNotEmpty().ValidatesForWhole(() => "ERROR");

            rules.Check("sp", null);

            Assert.AreEqual(1, rules.GetErrors("sp").Count());
            Assert.AreEqual(Rules.EMPTY_ERROR, rules.GetErrors("sp").First());
        }

        [TestMethod]
        public void CheckRuleOrdersDescending()
        {
            rules.MakeSure("sp").ValidatesForWhole(() => "ERROR").IsNotEmpty();

            rules.Check("sp", null);

            Assert.AreEqual(1, rules.GetErrors("").Count());
            Assert.AreEqual("ERROR", rules.GetErrors("").First());
        }

        [TestMethod]
        public void CheckMultipleProperties()
        {
            rules.MakeSure("prop1").IsNotEmpty();
            rules.MakeSure("prop2").IsNotEmpty().ValidatesForWhole(() => "ERR");

            rules.Check("prop1", string.Empty);
            rules.Check("prop2", null);

            Assert.AreEqual(1, rules.GetErrors("prop1").Count());
            Assert.AreEqual(1, rules.GetErrors("prop2").Count());
            Assert.AreEqual(Rules.EMPTY_ERROR, rules.GetErrors("prop1").First());
            Assert.AreEqual(Rules.EMPTY_ERROR, rules.GetErrors("prop2").First());
        }

        [TestMethod]
        public void Check_PropertyA_And_Error_PropertyB()
        {
            rules.MakeSure("prop1").ValidatesForWhole(() => "Err" );

            rules.Check("prop1", null);

            Assert.AreEqual(0, rules.GetErrors("prop1").Count());
            Assert.AreEqual(1, rules.GetErrors("").Count());
            Assert.AreEqual("Err", rules.GetErrors("").First());
        }

        [TestMethod]
        public void CheckNumberOfRaisedEvents()
        {
            var i = 0;
            rules = new Rules(x => i++);
            rules.MakeSure("p1").IsNotEmpty();
            rules.MakeSure("p2").IsNotEmpty().ValidatesForWhole(() => "ERROR");
            rules.MakeSure("p3").ValidatesForWhole(() => null);

            rules.Check("p1", "val");
            rules.Check("p1", string.Empty);
            rules.Check("p2", string.Empty);
            rules.Check("p2", "val");
            rules.Check("p3", null);

            Assert.AreEqual(3, i);
        }
    }
}