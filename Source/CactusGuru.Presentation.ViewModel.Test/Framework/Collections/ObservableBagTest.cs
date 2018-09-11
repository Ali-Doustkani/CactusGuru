using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Test.Framework.Collections
{
    public class Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ViewModel
    {
        public ViewModel(Dto dto)
        {
            Dto = dto;
        }

        public Dto Dto;

        public int Id
        {
            get { return Dto.Id; }
            set { Dto.Id = value; }
        }

        public string Name
        {
            get { return Dto.Name; }
            set { Dto.Name = value; }
        }
    }

    [TestClass]
    public class ObservableBagTest
    {
        [TestMethod]
        public void Add()
        {
            var col = Bag.Of<ViewModel>()
                .WithConvertor((Dto x) => new ViewModel(x))
                .WithId(x => x.Id)
                .Build();
            var added = new Dto { Id = 2, Name = "two" };

            col.Change(new NotificationEventArgs(added, OperationType.Add));

            Assert.AreEqual(1, col.Count);
            Assert.AreEqual(added, col.First().Dto);
        }

        [TestMethod]
        public void Add_WhenFiltered_Apply()
        {
            var source = new List<Dto>();
            source.Add(new Dto { Id = 1, Name = "ali" });
            source.Add(new Dto { Id = 2, Name = "haniye" });
            source.Add(new Dto { Id = 3, Name = "pooran" });
            var col = Bag.Of<Dto>()
                .FilterBy((item, text) => item.Name.Contains(text))
                .WithSource(source)
                .Build();
            col.FilterText = "poo";

            col.Add(new Dto { Id = 4, Name = "pooria" });

            Assert.AreEqual(2, col.Count);
        }

        [TestMethod]
        public void Edit()
        {
            var item1 = new ViewModel(new Dto { Id = 1, Name = "one" });
            var item2 = new ViewModel(new Dto { Id = 2, Name = "two" });
            var col = Bag.Of<ViewModel>()
                .WithConvertor((Dto x) => new ViewModel(x))
                .WithId(x => x.Id)
                .Loads(() => new ViewModel[] { item1, item2 })
                .Build();

            var item3 = new Dto { Id = 2, Name = "updated two" };
            col.Change(new NotificationEventArgs(item3, OperationType.Update));

            Assert.AreEqual(2, col.Count);
            Assert.AreEqual(2, col.Last().Id);
            Assert.AreEqual("updated two", col.Last().Name);
        }

        [TestMethod]
        public void EditWithTheSameType()
        {
            var old = new ViewModel(new Dto { Id = 1, Name = "one" });
            var col = Bag.Of<ViewModel>()
                .WithId(x => x.Id)
                .Loads(() => new ViewModel[] { old })
                .Build();

            var newItem = new ViewModel(new Dto { Id = 1, Name = "new one" });
            col.Change(new NotificationEventArgs(newItem, OperationType.Update));

            Assert.AreEqual(1, col.Last().Id);
            Assert.AreEqual("new one", col.Last().Name);
        }

        [TestMethod]
        public void Delete()
        {
            var item1 = new ViewModel(new Dto { Id = 1, Name = "one" });
            var item2 = new ViewModel(new Dto { Id = 2, Name = "two" });
            var col = Bag.Of<ViewModel>()
                .WithConvertor((Dto x) => new ViewModel(x))
                .WithId(x => x.Id)
                .Loads(() => new ViewModel[] { item1, item2 })
                .Build();

            var deletedItem = new Dto { Id = 1, Name = "one" };
            col.Change(new NotificationEventArgs(deletedItem, OperationType.Delete));

            Assert.AreEqual(1, col.Count);
            Assert.AreEqual("two", col.First().Name);
        }

        [TestMethod]
        public void Filter()
        {
            var source = new Dto[]
            {
                new Dto{ Id= 1, Name="abcd"},
                new Dto{ Id= 2, Name="bab"},
                new Dto{ Id= 3, Name="bbd"},
                new Dto{ Id= 4, Name="dba"},
                new Dto{ Id= 5, Name="ccc"}
            };
            var col = Bag.Of<Dto>()
                .WithSource(source)
                .FilterBy((item, text) => item.Name.Contains(text))
                .Build();

            col.FilterText = "a";

            Assert.AreEqual(3, col.Count);
        }
    }
}
