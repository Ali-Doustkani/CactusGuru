using CactusGuru.Infrastructure;

namespace CactusGuru.Domain.Greenhouse
{
    public class Supplier : DomainEntity
    {
        public string FullName { get; set; }
        public string WebSite { get; set; }
        public string Acronym { get; set; }

        public string Format()
        {
            if (string.IsNullOrEmpty(Acronym))
                return FullName;
            return $"{FullName}({Acronym})";
        }

        public override string ToString()
        {
            return FullName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Supplier)) return false;
            return Equals(obj as Supplier);
        }

        public virtual bool Equals(Supplier other)
        {
            if (other == null) return false;
            if (other is NullSupplier) return false;
            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #region NULL OBJECT

        private static NullSupplier _empty;
        public static Supplier Empty
        {
            get { return _empty ?? (_empty = new NullSupplier()); }
        }

        private class NullSupplier : Supplier
        {
            public override bool Equals(Supplier other)
            {
                return other is NullSupplier;
            }

            public override string ToString()
            {
                return "EMPTY SUPPLIER";
            }
        }

        #endregion
    }
}
