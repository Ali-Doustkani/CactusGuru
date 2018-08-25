using CactusGuru.Infrastructure;

namespace CactusGuru.Domain.Greenhouse
{
    public class Genus : DomainEntity
    {
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public virtual bool Equals(Genus other)
        {
            if (other == null) return false;
            if (other is NullGenera) return false;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Genus);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #region NULL OBJECT

        private static NullGenera _empty;
        public static Genus Empty
        {
            get { return _empty ?? (_empty = new NullGenera()); }
        }

        private class NullGenera : Genus
        {
            public override bool Equals(Genus other)
            {
                return other is NullGenera;
            }

            public override string ToString()
            {
                return "EMPTY Genera";
            }
        }

        #endregion
    }
}
