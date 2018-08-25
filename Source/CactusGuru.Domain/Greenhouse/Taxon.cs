using System;
using CactusGuru.Infrastructure;

namespace CactusGuru.Domain.Greenhouse
{
    public class Taxon : DomainEntity, IEquatable<Taxon>
    {
        public static Taxon UnknownSpeciesOf(Genus Genera)
        {
            var ret = new Taxon();
            ret.Genus = Genera;
            ret.Species = "spec.";
            return ret;
        }

        public Taxon()
        {
            Genus = Genus.Empty;
        }

        public virtual string Species { get; set; }

        public virtual string Variety { get; set; }

        public virtual string SubSpecies { get; set; }

        public virtual string Forma { get; set; }

        public virtual string Cultivar { get; set; }

        public Guid GeneraId { get; set; }

        private Genus _genus;
        public virtual Genus Genus
        {
            get { return _genus; }
            set
            {
                if (value == null)
                {
                    _genus = Genus.Empty;
                    GeneraId = Guid.Empty;
                }
                else
                {
                    _genus = value;
                    GeneraId = value.Id;
                }
            }
        } 

        #region IEqutable

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Taxon)) return false;
            return Equals(obj as Taxon);
        }

        public virtual bool Equals(Taxon other)
        {
            if (other == null) return false;
            if (other is NullTaxon) return false;
            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region METHODS

        public override string ToString()
        {
            if (Genus == Genus.Empty)
                return String.Empty;
            return String.Format("{0} {1}", Genus, Species);
        }

        #endregion

        #region NULL OBJECT

        private static NullTaxon _empty;
        public static Taxon Empty
        {
            get { return _empty ?? (_empty = new NullTaxon()); }
        }

        private class NullTaxon : Taxon
        {
            public override Genus Genus
            {
                get { return Genus.Empty; }
                set { }
            }

            public override string Cultivar
            {
                get { return String.Empty; }
                set { }
            }

            public override string Species
            {
                get { return String.Empty; }
                set { }
            }

            public override string SubSpecies
            {
                get { return String.Empty; }
                set { }
            }

            public override string Variety
            {
                get { return String.Empty; }
                set { }
            }

            public override string Forma
            {
                get { return String.Empty; }
                set { }
            }

            public override bool Equals(Taxon other)
            {
                return other is NullTaxon;
            }

            public override string ToString()
            {
                return "EMPTY TAXON";
            }
        }

        #endregion
    }
}
