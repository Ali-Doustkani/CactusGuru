using CactusGuru.Infrastructure.Utils;

namespace CactusGuru.Domain.Greenhouse.Formatting.CollectionItems
{
    public class RefCollectionItemFormatter : IFormatter<CollectionItem>
    {
        public string Format(CollectionItem collectionItem)
        {
            var ret = GetSupplierRef(collectionItem);
            ret = GetIncomeDateRef(collectionItem, ret);
            if (string.IsNullOrEmpty(ret))
                return string.Empty;
            ret = GetIncomeTypeRef(collectionItem, ret);
            if (!string.IsNullOrEmpty(ret))
                return $"({ret})";
            return string.Empty;
        }

        private string GetSupplierRef(CollectionItem collectionItem)
        {
            if (collectionItem.Supplier.Equals(Supplier.Empty))
                return string.Empty;

            var supplierName = string.Empty;
            if (collectionItem.Supplier.HasAcronym())
                supplierName = collectionItem.Supplier.Acronym;
            else
                supplierName = collectionItem.Supplier.FullName;

            if (collectionItem.HasSupplierCode())
                return supplierName + "-" + collectionItem.SupplierCode;

            return supplierName;
        }

        private string GetIncomeDateRef(CollectionItem collectionItem, string reference)
        {
            if (!collectionItem.IncomeDate.HasValue)
                return reference;
            var incomeYear = DateUtil.GetPersianYear(collectionItem.IncomeDate.Value);
            if (!string.IsNullOrEmpty(reference))
                return reference + "-" + incomeYear;
            return incomeYear.ToString();
        }

        private string GetIncomeTypeRef(CollectionItem collectionItem, string reference)
        {
            if (collectionItem.IncomeType == IncomeType.None)
                return string.Empty;
            var incomeType = collectionItem.IncomeType == IncomeType.Plant ? "P" : "S";
            return reference + "-" + incomeType;
        }
    }
}
