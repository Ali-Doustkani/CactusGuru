using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Infrastructure;
using System;

namespace CactusGuru.Domain.Greenhouse
{
    public class CollectionItem : DomainEntity, IEquatable<CollectionItem>
    {
        public CollectionItem()
        {
            _taxon = Taxon.Empty;
            _collector = Collector.Empty;
            _supplier = Supplier.Empty;
        }

        public string Code { get; set; }
        public int? Count { get; set; }
        public string FieldNumber { get; set; }
        public string SupplierCode { get; set; }
        public string Locality { get; set; }
        public DateTime? IncomeDate { get; set; }
        public IncomeType IncomeType { get; set; }
        public string Description { get; set; }

        private Taxon _taxon;
        public Taxon Taxon
        {
            get { return _taxon; }
            set
            {
                _taxon = value;
                if (value == null)
                    _taxon = Taxon.Empty;
            }
        }

        private Collector _collector;
        public Collector Collector
        {
            get { return _collector; }
            set
            {
                _collector = value;
                if (value == null)
                    _collector = Collector.Empty;
            }
        }

        private Supplier _supplier;
        public Supplier Supplier
        {
            get { return _supplier; }
            set
            {
                _supplier = value;
                if (value == null)
                    _supplier = Supplier.Empty;
            }
        }

        public bool HasSupplierCode()
        {
            return !string.IsNullOrEmpty(SupplierCode);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is CollectionItem)) return false;
            return Equals(obj as CollectionItem);
        }

        public virtual bool Equals(CollectionItem other)
        {
            if (other == null) return false;
            if (other is NullCollectionItem) return false;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public string ToString(IFormatter<CollectionItem> formatter)
        {
            return formatter.Format(this);
        }

        #region NULL OBJECT

        private static NullCollectionItem _empty;

        public static CollectionItem Empty
        {
            get { return _empty ?? (_empty = new NullCollectionItem()); }
        }

        private class NullCollectionItem : CollectionItem
        {
            public override bool Equals(CollectionItem other)
            {
                return other is NullCollectionItem;
            }

            public override string ToString()
            {
                return "EMPTY COLLECTION ITEM";
            }
        }

        #endregion
    }
}
