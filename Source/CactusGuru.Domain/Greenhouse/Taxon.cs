using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Utils;
using System;

namespace CactusGuru.Domain.Greenhouse
{
    public class Taxon : DomainEntity, IEquatable<Taxon>
    {
        public static Taxon UnknownSpeciesOf(Genus genus)
        {
            var ret = new Taxon();
            ret.Genus = genus;
            ret.Species = "spec.";
            return ret;
        }

        public Taxon()
        {
            Genus = Genus.Empty;
            _species = string.Empty;
            _variety = string.Empty;
            _subSpecies = string.Empty;
            _forma = string.Empty;
            _cultivar = string.Empty;
        }

        private string _species;
        public virtual string Species
        {
            get { return _species; }
            set { SetString(ref _species, value); }
        }

        private string _variety;
        public virtual string Variety
        {
            get { return _variety; }
            set { SetString(ref _variety, value); }
        }

        private string _subSpecies;
        public virtual string SubSpecies
        {
            get { return _subSpecies; }
            set { SetString(ref _subSpecies, value); }
        }

        private string _forma;
        public virtual string Forma
        {
            get { return _forma; }
            set { SetString(ref _forma, value); }
        }

        private string _cultivar;
        public virtual string Cultivar
        {
            get { return _cultivar; }

            set { SetString(ref _cultivar, value); }
        }

        private Genus _genus;
        public virtual Genus Genus
        {
            get { return _genus; }
            set { Genus.SetGenus(ref _genus, value); }
        }

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

        public override string ToString()
        {
            if (Genus == Genus.Empty)
                return String.Empty;
            return String.Format($"{Genus} {Species}");
        }

        public virtual string Format(string format)
        {
            return StringFormatting.Tokenize(format, token => FormatByToken(token));
        }

        public string FormatByToken(string token)
        {
            if (IsGenus(token))
                return Genus.Format(token);
            else if (IsTaxon(token))
                return FormatTaxon();
            throw new Exception("Invalid token");
        }

        public static bool IsMyToken(string token)
        {
            return IsGenus(token) || IsTaxon(token);
        }

        private static bool IsGenus(string token)
        {
            return token.ToLower() == "genus";
        }

        private static bool IsTaxon(string token)
        {
            return token.ToLower() == "taxon";
        }

        private string FormatTaxon()
        {
            if (string.IsNullOrEmpty(Species))
                return string.Empty;
            var displayName = Species.Trim().ToLower();
            return ConcatToSubSpecies(displayName);
        }

        private string ConcatToSubSpecies(string displayName)
        {
            var sspVar = GetSubSpecies();
            if (!string.IsNullOrEmpty(sspVar))
                displayName = $"{displayName}{sspVar}";
            return displayName;
        }

        private string GetSubSpecies()
        {
            var ret = string.Empty;
            if (!string.IsNullOrEmpty(SubSpecies))
                ret = $"{ret} ssp. {SubSpecies}";
            if (!string.IsNullOrEmpty(Variety))
                ret = $"{ret} var. {Variety}";
            if (!string.IsNullOrEmpty(Forma))
                ret = $"{ret} fa. {Forma}";
            if (!string.IsNullOrEmpty(Cultivar))
                ret = $"{ret} cv. {Genus.CapitalizeFirstLetter(Cultivar)}";
            return ret;
        }

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

            public override string Format(string format)
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
