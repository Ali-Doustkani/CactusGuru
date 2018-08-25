using System.Linq;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Sales;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CactusGuru.Domain.Test.Greenhouse.Sales
{
    [TestClass]
    public class SeedListTest
    {
        private SeedList _seedList;

        [TestInitialize]
        public void SetUp()
        {
            _seedList = new SeedList();
        }

        [TestMethod]
        public void Ctor()
        {
            Assert.AreEqual(DateTime.Now.Year, _seedList.PublishDate.Year);
            Assert.AreEqual(DateTime.Now.Month, _seedList.PublishDate.Month);
            Assert.AreEqual(DateTime.Now.Day, _seedList.PublishDate.Day);
            Assert.AreEqual("SeedListTest", _seedList.Name);
        }

        [TestMethod]
        public void AddItem_AddCollectionItem()
        {
            var colItem = new CollectionItem();
            colItem.Id = Guid.NewGuid();
            colItem.Code = "1";
            var item = new CollectionSeedListItem(colItem);
            _seedList.AddItem(item);

            Assert.AreEqual(1, _seedList.Items.Count());
        }

        [TestMethod]
        public void GenerateSupplierItemCode()
        {
            var code = _seedList.GenerateSupplierItemCode();

            Assert.AreEqual("N1", code);
        }

        [TestMethod]
        public void GenerateSupplierItemCode_WhenOtherItemsExists()
        {
            var taxon = new Taxon();
            taxon.Id = Guid.NewGuid();
            var item = new SupplierSeedListItem("N1", taxon);
            _seedList.AddItem(item);

            Assert.AreEqual("N2", _seedList.GenerateSupplierItemCode());
        }

        [TestMethod]
        public void RemoveItem()
        {
            var colItem = new CollectionItem();
            colItem.Id = Guid.NewGuid();
            colItem.Code = "1";
            var item = new CollectionSeedListItem(colItem);
            _seedList.AddItem(item);

            _seedList.RemoveItem(item);

            Assert.AreEqual(0, _seedList.Items.Count());
        }

        [TestMethod]
        public void ClearItems()
        {
            var colItem = new CollectionItem();
            colItem.Id = Guid.NewGuid();
            colItem.Code = "1";
            var item = new CollectionSeedListItem(colItem);
            _seedList.AddItem(item);

            _seedList.ClearItems();

            Assert.AreEqual(0, _seedList.Items.Count());
        }
    }
}
