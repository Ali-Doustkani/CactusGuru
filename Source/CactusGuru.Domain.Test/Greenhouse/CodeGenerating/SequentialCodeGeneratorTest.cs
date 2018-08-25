using System.Collections.Generic;
using CactusGuru.Domain.Greenhouse.CodeGenerating;
using CactusGuru.Domain.Persistance.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CactusGuru.Domain.Test.Greenhouse.CodeGenerating
{
    [TestClass]
    public class SequentialCodeGeneratorTest
    {
        private SequentialCodeGenerator _generator;
        private Mock<ICollectionItemRepository> _repository;

        [TestInitialize]
        public void SetUp()
        {
            _repository = new Mock<ICollectionItemRepository>();
            _generator = new SequentialCodeGenerator(_repository.Object);
        }

        [TestMethod]
        public void Generate()
        {
            _repository.Setup(x => x.GetAllCodes()).Returns(new[] { "3", "4" });

            Assert.AreEqual("5", _generator.Generate());
        }

        [TestMethod]
        public void Generate_IfThereIsNoCode_Return1()
        {
            _repository.Setup(x => x.GetAllCodes()).Returns(new List<string>());

            Assert.AreEqual("1", _generator.Generate());
        }

        [TestMethod]
        public void Generate_IfThereIsNoneIntegerCodes_IgnoreThem()
        {
            _repository.Setup(x => x.GetAllCodes()).Returns(new[] {"1", "2", "2.1"});

            Assert.AreEqual("3", _generator.Generate());
        }
    }
}
