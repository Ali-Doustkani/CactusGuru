using CactusGuru.Domain.Greenhouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CactusGuru.Domain.Test.Greenhouse
{
    [TestClass]
    public class CollectionItemTest
    {
        private CollectionItem _collectionItem;

        [TestInitialize]
        public void Setup()
        {
            _collectionItem = new CollectionItem();
        }

        private void Plant(string genus, string species, string collector = null, string field = null, string locality = null)
        {
            _collectionItem.Taxon = new Taxon
            {
                Genus = new Genus { Title = genus },
                Species = species
            };
            _collectionItem.Collector = new Collector { Acronym = collector };
            _collectionItem.FieldNumber = field;
            _collectionItem.Locality = locality;
        }

        private void Supplier(string acronym, string code = null)
        {
            _collectionItem.Supplier = new Supplier { Acronym = acronym };
            _collectionItem.SupplierCode = code;
        }

        private void Date(int year)
        {
            _collectionItem.IncomeDate = new DateTime(year, 1, 1);
        }

        private void Type(IncomeType type)
        {
            _collectionItem.IncomeType = type;
        }

        [TestMethod]
        public void ToStringOfReference_Format1()
        {
            Plant("lobivia", "ferox");
            Supplier("MG", "12.36");
            Date(2015);
            Type(IncomeType.Plant);

            AssertFormat("(MG-12.36-1393-P)",  "{ref}");
        }

        [TestMethod]
        public void ToStringOfReference_Format2()
        {
            Plant("lobivia", "ferox");
            Supplier("IRKS");
            Date(2015);
            Type(IncomeType.Plant);

            AssertFormat("(IRKS-1393-P)",  "{ref}");
        }

        [TestMethod]
        public void ToStringOfReference_Format3()
        {
            Plant("lobivia", "ferox");
            Supplier("IRAA");
            Type(IncomeType.Plant);

            AssertFormat("(IRAA-P)", "{ref}");
        }

        [TestMethod]
        public void ToStringOfReference_Format4()
        {
            Plant("lobivia", "ferox");
            Supplier("MG", "12.36");

            AssertFormat("(MG-12.36-S)", "{ref}");
        }

        [TestMethod]
        public void ToStringOfReference_Format5()
        {
            Type(IncomeType.Plant);

            AssertFormat(string.Empty, "{ref}");
        }

        [TestMethod]
        public void ToStringOfReference_Format6()
        {
            Plant("lobivia", "ferox");
            Date(2015);

            AssertFormat("(1393-S)", "{ref}");
        }

        [TestMethod]
        public void ToString_WithoutTaxon_EmptyString()
        {
            var item = new CollectionItem { Taxon = null };

            Assert.IsTrue(string.IsNullOrEmpty(item.Format("{GENUS} {taxon}")));
        }

        [TestMethod]
        public void ToString_WithTaxon()
        {
            Plant("astrophytum", "asterias");

            AssertFormat("ASTROPHYTUM asterias",  "{GENUS} {taxon}");
        }

        [TestMethod]
        public void ToString_CompleteInformation()
        {
            Plant("astrophytum", "asterias", "L", "80", "BC");

            AssertFormat("ASTROPHYTUM asterias L80, BC",  "{GENUS} {taxon} {field}, {locality}");
        }

        [TestMethod]
        public void ToString_WithoutLocality()
        {
            Plant("lobivia", "ferox");

            AssertFormat("LOBIVIA ferox", "{GENUS} {taxon}{, locality}");
        }

        [TestMethod]
        public void ToString_JustLocality()
        {
            Plant("astrophytum", "asterias", null, null, "Mexico");

            AssertFormat("Mexico", "{locality}");
        }

        private void AssertFormat(string expected, string format)
        {
            Assert.AreEqual(expected, _collectionItem.Format(format));
        }
    }
}
