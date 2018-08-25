using CactusGuru.Domain.Greenhouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using CactusGuru.Domain.Greenhouse.Qualification.Validators;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Domain.Test.Greenhouse.Qualification.Validators
{
    [TestClass]
    public class CollectionItemValidatorTest
    {
        private CollectionItemValidator _validator;
        private Mock<IEmptySpecification> _emptySpec;
        private CollectionItem _collectionItem;

        [TestInitialize]
        public void SetUp()
        {
            var mock = new Mock<CollectionItem>();
            mock.SetupAllProperties();
            _collectionItem = mock.Object;
            _emptySpec = new Mock<IEmptySpecification>();
            _validator = new CollectionItemValidator(_emptySpec.Object);
        }

        [TestMethod]
        public void IfSupplierIsEmpty_CanNotSetSupplierCode()
        {
            _collectionItem.Supplier = Supplier.Empty;
            _collectionItem.SupplierCode = "12.34";

            try
            {
                _validator.Validate(_collectionItem);
                Assert.Fail();
            }
            catch (ErrorHappenedException ex)
            {
                CollectionAssert.Contains(ex.Errors.ToList(), CollectionItemValidator.ERROR_SUPPLIER_CODE);
            }
        }
    }
}
