using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Qualification.Inquiries;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace CactusGuru.Domain.Test.Greenhouse.Qualification.Inquiries
{
    [TestClass]
    public class TaxonInquiryTest
    {
        private TaxonInquiry _inquiry;
        private Taxon _taxon;
        private Mock<ICollectionItemRepository> _repo;

        [TestInitialize]
        public void SetUp()
        {
            _taxon = new Taxon { Id = Guid.NewGuid() };
            _repo = new Mock<ICollectionItemRepository>();
            _inquiry = new TaxonInquiry(_repo.Object);
        }

        [TestMethod]
        public void Inquiry_CheckCollectionItems()
        {
            var items = new List<CollectionItem>();
            var taxon = new Taxon();
            taxon.Genus = new Genus();
            taxon.Genus.Title = "sdf";
            var item = new Mock<CollectionItem>();
            item.SetupAllProperties();
            item.Object.Taxon = taxon;
            item.Object.Code = "aa";
            items.Add(item.Object);
            _repo.Setup(x => x.GetByTaxonId(It.IsAny<Guid>())).Returns(items);

            try
            {
                _inquiry.Inquiry(_taxon.Id);
            }
            catch(ErrorHappenedException ex)
            {
                Assert.IsTrue(ex.Errors.Count == 1);
            }
        }
    }
}
