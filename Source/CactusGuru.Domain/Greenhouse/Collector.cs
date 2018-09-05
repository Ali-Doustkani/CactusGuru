using CactusGuru.Infrastructure;
using System;

namespace CactusGuru.Domain.Greenhouse
{
    public class Collector : DomainEntity, IEquatable<Collector>
    {
        public Collector()
        {
            _fullName = string.Empty;
            _acronym = string.Empty;
            _webSite = string.Empty;
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { SetString(ref _fullName, value); }
        }

        private string _acronym;
        public string Acronym
        {
            get { return _acronym; }
            set { SetString(ref _acronym, value); }
        }

        private string _webSite;
        public string WebSite
        {
            get { return _webSite; }
            set { SetString(ref _webSite, value); }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Collector)) return false;
            return Equals(obj as Collector);
        }

        public virtual bool Equals(Collector other)
        {
            if (other == null) return false;
            if (other is NullCollector) return false;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return FullName;
        }

        public string Format()
        {
            if (string.IsNullOrEmpty(Acronym))
                return FullName;
            return $"{FullName}({Acronym})";
        }

        #region NULL OBJECT

        private static NullCollector _empty;

        public static Collector Empty
        {
            get { return _empty ?? (_empty = new NullCollector()); }
        }

        private class NullCollector : Collector
        {
            public override bool Equals(Collector other)
            {
                return other is NullCollector;
            }

            public override string ToString()
            {
                return "EMPTY COLLECTOR";
            }

        }

        #endregion
    }
}
