using System;
using System.Collections.Generic;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Persistance.Merging;
using CactusGuru.Infrastructure.Qualification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CactusGuru.Infrastructure.Test.Persistance.Merging
{
    [TestClass]
    public class MergerTest
    {
        private Publisher<TestDomainEntity> _publisher;
        private Mock<IRepository<TestDomainEntity>> _repo;
        private Mock<ITerminator<TestDomainEntity>> _terminator;
        private Merger<TestDomainEntity> _merger;

        [TestInitialize]
        public void Setup()
        {
            _repo = new Mock<IRepository<TestDomainEntity>>();
            _publisher = new Publisher<TestDomainEntity>(_repo.Object, new Mock<ValidatorBase<TestDomainEntity>>().Object);
            _terminator = new Mock<ITerminator<TestDomainEntity>>();
            _merger = new Merger<TestDomainEntity>(_publisher, _terminator.Object);
        }

        [TestMethod]
        public void DeleteItems()
        {
            var item1 = new TestDomainEntity(Guid.NewGuid());
            var item2 = new TestDomainEntity(Guid.NewGuid());
            var item3 = new TestDomainEntity(Guid.NewGuid());

            var originals = new List<TestDomainEntity>();
            originals.Add(item1);
            originals.Add(item2);
            originals.Add(item3);
            var currentItems = new List<TestDomainEntity>();
            currentItems.Add(item1);
            currentItems.Add(item2);

            _merger.Merge(originals, currentItems);

            _terminator.Verify(x => x.Terminate(item3.Id), Times.Once);
            _repo.Verify(x => x.Add(It.IsAny<TestDomainEntity>()), Times.Never);
        }

        [TestMethod]
        public void AddItems()
        {
            var item1 = new TestDomainEntity(Guid.NewGuid());
            var item2 = new TestDomainEntity(Guid.NewGuid());
            var item3 = new TestDomainEntity(Guid.NewGuid());

            var originals = new List<TestDomainEntity>();
            originals.Add(item1);
            originals.Add(item2);
            var currentItems = new List<TestDomainEntity>();
            currentItems.Add(item1);
            currentItems.Add(item2);
            currentItems.Add(item3);

            _merger.Merge(originals, currentItems);

            _repo.Verify(x => x.Add(item3), Times.Once);
            _terminator.Verify(x => x.Terminate(It.IsAny<Guid>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItems()
        {
            var item1 = new TestDomainEntity(Guid.NewGuid());
            var item2 = new TestDomainEntity(Guid.NewGuid());
            var item3 = new TestDomainEntity(Guid.NewGuid());

            var originals = new List<TestDomainEntity>();
            originals.Add(item1);
            originals.Add(item2);
            originals.Add(item3);
            var currentItems = new List<TestDomainEntity>();
            currentItems.Add(item1);
            currentItems.Add(item2);
            currentItems.Add(item3);

            _merger.Merge(originals, currentItems);

            _repo.Verify(x => x.Update(item1), Times.Once);
            _repo.Verify(x => x.Update(item2), Times.Once);
            _repo.Verify(x => x.Update(item3), Times.Once);
        }
    }

    public class TestDomainEntity : DomainEntity
    {
        public TestDomainEntity(Guid id)
        {
            Id = id;
        }
    }
}
