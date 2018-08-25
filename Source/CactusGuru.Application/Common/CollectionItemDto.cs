using System;

namespace CactusGuru.Application.Common
{
    public class CollectionItemDto : TransferObjectBase
    {
        public string Code { get; set; }
        public int? Count { get; set; }
        public string FieldNumber { get; set; }
        public string SupplierCode { get; set; }
        public string Locality { get; set; }
        public DateTime? IncomeDate { get; set; }
        public IncomeTypeDto IncomeType { get; set; }
        public string Description { get; set; }
        public Guid? Taxon { get; set; }
        public Guid? Collector { get; set; }
        public Guid? Supplier { get; set; }
    }
}
