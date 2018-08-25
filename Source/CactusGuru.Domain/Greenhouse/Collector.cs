using CactusGuru.Infrastructure;
using System;

namespace CactusGuru.Domain.Greenhouse
{
    public class Collector : DomainEntity, IEquatable<Collector>
    {
        public string FullName { get; set; }
        public string Acronym { get; set; }
        public string WebSite { get; set; }

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
