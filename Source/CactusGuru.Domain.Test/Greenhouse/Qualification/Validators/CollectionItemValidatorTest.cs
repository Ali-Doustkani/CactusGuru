using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Qualification.Validators;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Qualification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

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
            _collectionItem = new CollectionItem();
            var noErrSpec = new Mock<ISpecification>();
            noErrSpec.Setup(x => x.Satisfy(It.IsAny<DomainEntity>())).Returns(Error.Empty);
            _emptySpec = new Mock<IEmptySpecification>();
            _emptySpec.Setup(x => x.SetProperty(It.IsAny<string>())).Returns(noErrSpec.Object);
            _validator = new CollectionItemValidator(_emptySpec.Object);
        }

        [TestMethod]
        public void IfSupplierIsEmptyAndCodeIsNot_Err()
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
                Assert.IsTrue(ex.Errors.Any(x => x.Message == CollectionItemValidator.ERROR_SUPPLIER_CODE));
            }
        }
    }
}
