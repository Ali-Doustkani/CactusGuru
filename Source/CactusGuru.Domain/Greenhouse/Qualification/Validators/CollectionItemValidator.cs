using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Domain.Greenhouse.Qualification.Validators
{
    public class CollectionItemValidator : ValidatorBase<CollectionItem>
    {
        public CollectionItemValidator(IEmptySpecification emptySpec)
        {
            _emptySpec = emptySpec;
        }

        public static string ERROR_SUPPLIER_CODE = "Supplier cannot be empty when you have entered the 'Supplier Code'.";

        private readonly IEmptySpecification _emptySpec;

        protected override ErrorCollection ValidateImp(CollectionItem domainEntity)
        {
            var ret = new ErrorCollection();
            ret.Add(_emptySpec.SetProperty(nameof(CollectionItem.Taxon)).Satisfy(domainEntity));
            ret.Add(_emptySpec.SetProperty(nameof(CollectionItem.Code)).Satisfy(domainEntity));
            ret.Add(SupplierIsEmptyAndCodeIsNot(domainEntity));
            return ret;
        }

        private Error SupplierIsEmptyAndCodeIsNot(CollectionItem domainEntity)
        {
            var supplierIsEmpty = domainEntity.Supplier == null || domainEntity.Supplier.Equals(Supplier.Empty);
            if (supplierIsEmpty && !string.IsNullOrEmpty(domainEntity.SupplierCode))
                return new Error(ERROR_SUPPLIER_CODE);
            return Error.Empty;
        }
    }
}
