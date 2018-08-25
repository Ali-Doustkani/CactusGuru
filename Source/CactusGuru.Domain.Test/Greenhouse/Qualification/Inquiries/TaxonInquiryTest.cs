using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Greenhouse.Qualification.Inquiries;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Persistance;
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
        private Mock<IUnitOfWork> _uow;

        [TestInitialize]
        public void SetUp()
        {
            _taxon = new Taxon { Id = Guid.NewGuid() };
            _uow = new Mock<IUnitOfWork>();
            _uow.DefaultValue = DefaultValue.Mock;
            _inquiry = new TaxonInquiry(_uow.Object, Mock.Of<IFormatter<CollectionItem>>());
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
            var repo = Mock.Get(_uow.Object.CreateRepository<ICollectionItemRepository>());
            repo.Setup(x => x.GetByTaxonId(It.IsAny<Guid>())).Returns(items);

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
