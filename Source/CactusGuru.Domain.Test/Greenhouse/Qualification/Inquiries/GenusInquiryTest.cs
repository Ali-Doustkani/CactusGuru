﻿using CactusGuru.Domain.Greenhouse;
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
    public class GeneraInquiryTest
    {
        private GeneraInquiry _inq;
        private Genus _Genera;
        private Mock<ITaxonRepository> _repo;

        [TestInitialize]
        public void SetUp()
        {
            _repo = new Mock<ITaxonRepository>();
            _Genera = new Genus();
            _Genera.Id = Guid.NewGuid();
            _inq = new GeneraInquiry(_repo.Object);
        }

        [TestMethod]
        public void Inquiry_CheckTaxa()
        {
            var taxa = new List<Taxon>();
            var Genera = new Genus();
            Genera.Title = "Astrophytum";
            var t1 = new Taxon
            {
                Genus = Genera,
                Species = "myriostigma"
            };
            taxa.Add(t1);
            _repo.Setup(x => x.GetByGeneraId(It.IsAny<Guid>())).Returns(taxa);

            try
            {
                _inq.Inquiry(_Genera.Id);
            }
            catch (ErrorHappenedException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
            }
        }
    }
}
