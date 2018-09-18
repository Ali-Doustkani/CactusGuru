using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Utils;
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

        public virtual string Format(string format)
        {
            if (Taxon == Taxon.Empty)
                return string.Empty;

            return StringFormatting.Tokenize(format, token =>
            {
                if (Taxon.IsMyToken(token))
                    return Taxon.FormatByToken(token);
                else if (IsCodeToken(token))
                    return Code;
                else if (IsRefToken(token))
                    return Reference();
                else if (IsFieldToken(token))
                    return Field();
                else if (IsLocalityToken(token))
                    return Locality;
                throw new Exception("Invalid token");
            });
        }

        private bool IsCodeToken(string token)
        {
            return token.Equals("code", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsRefToken(string token)
        {
            return token.Equals("ref", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsFieldToken(string token)
        {
            return token.Equals("field", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsLocalityToken(string token)
        {
            return token.Equals("locality", StringComparison.OrdinalIgnoreCase);
        }

        private string Reference()
        {
            var ret = GetSupplierRef();
            ret = GetIncomeDateRef(ret);
            if (string.IsNullOrEmpty(ret))
                return string.Empty;
            ret = GetIncomeTypeRef(ret);
            if (!string.IsNullOrEmpty(ret))
                return $"({ret})";
            return string.Empty;
        }

        private string GetSupplierRef()
        {
            if (Supplier.Equals(Supplier.Empty))
                return string.Empty;

            var supplier = string.IsNullOrEmpty(Supplier.Acronym) ? Supplier.FullName : Supplier.Acronym;
            if (HasSupplierCode())
                return $"{supplier}-{SupplierCode}"; 

            return supplier;
        }

        private string GetIncomeDateRef(string reference)
        {
            if (!IncomeDate.HasValue)
                return reference;
            var incomeYear = IncomeDate.Value.Year;
            if (!string.IsNullOrEmpty(reference))
                return reference + "-" + incomeYear;
            return incomeYear.ToString();
        }

        private string GetIncomeTypeRef(string reference)
        {
            if (IncomeType == IncomeType.None)
                return string.Empty;
            var incomeType = IncomeType == IncomeType.Plant ? "P" : "S";
            return reference + "-" + incomeType;
        }

        private string Field()
        {
            return $"{Collector.Acronym}{FieldNumber}";
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

            public override string Format(string format)
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
