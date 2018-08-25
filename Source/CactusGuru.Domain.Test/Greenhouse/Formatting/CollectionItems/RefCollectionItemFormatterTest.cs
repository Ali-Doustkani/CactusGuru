using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting.CollectionItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CactusGuru.Domain.Test.Greenhouse.Formatting.CollectionItems
{
    [TestClass]
    public class RefCollectionItemFormatterTest
    {
        private RefCollectionItemFormatter _formatter;
        private CollectionItem _item;

        [TestInitialize]
        public void SetUp()
        {
            _formatter = new RefCollectionItemFormatter();
            _item = new CollectionItem();
        }

        private void Supplier(string  acronym, string code = null)
        {
            _item.Supplier = new Supplier { Acronym = acronym };
            _item.SupplierCode = code;
        }

        private void Date(int year)
        {
            _item.IncomeDate = new DateTime(year, 12, 20);
        }

        private void Plant()
        {
            _item.IncomeType = IncomeType.Plant;
        }

        private string Format()
        {
            return _formatter.Format(_item); 
        }

        [TestMethod]
        public void Format1()
        {
            Supplier("MG", "12.36");
            Date(2015);
            Plant();

            var formatted = Format();

            Assert.AreEqual("(MG-12.36-1394-P)", formatted);
        }

        [TestMethod]
        public void Format2()
        {
            Supplier("IRKS");
            Date(2015);
            Plant();

            var formatted = Format();

            Assert.AreEqual("(IRKS-1394-P)", formatted);
        }

        [TestMethod]
        public void Format3()
        {
            Supplier("IRAA");
            Plant();

            var formatted = Format();

            Assert.AreEqual("(IRAA-P)", formatted);
        }

        [TestMethod]
        public void Format4()
        {
            Supplier("MG", "12.36");

            var formatted = Format();

            Assert.AreEqual("(MG-12.36-S)", formatted);
        }

        [TestMethod]
        public void Format5()
        {
            Plant();

            var result = Format();

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Format6()
        {
            Date(2015);

            var result = Format();

            Assert.AreEqual("(1394-S)", result);
        }
    }
}
