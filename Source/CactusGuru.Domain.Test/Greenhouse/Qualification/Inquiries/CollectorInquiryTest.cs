﻿using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Qualification.Inquiries;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Domain.Test.Greenhouse.Qualification.Inquiries
{
    [TestClass]
    public class CollectorInquiryTest
    {
        private CollectorInquiry _inq;
        private Collector _collector;
        private Mock<ICollectionItemRepository> _repo;

        [TestInitialize]
        public void SetUp()
        {
            _repo = new Mock<ICollectionItemRepository>();
            _collector = new Collector();
            _collector.Id = Guid.NewGuid();
            _inq = new CollectorInquiry(_repo.Object);
        }

        [TestMethod]
        public void Inquiry_CheckCollectionItem()
        {
            var collectionItems = new List<CollectionItem>();
            var collectionItem = new CollectionItem();
            collectionItem.Taxon = new Taxon();
            collectionItem.Taxon.Genus = new Genus();
            collectionItem.Taxon.Genus.Title = "astrophytum";
            collectionItem.Taxon.Species = "capricorn";
            collectionItems.Add(collectionItem);
            _repo.Setup(x => x.GetByCollectorId(It.IsAny<Guid>())).Returns(collectionItems);

            try
            {
                _inq.Inquiry(_collector.Id);


            }
            catch (ErrorHappenedException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count());
            }
        }
    }
}
