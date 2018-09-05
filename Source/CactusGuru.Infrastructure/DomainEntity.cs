using System;

namespace CactusGuru.Infrastructure
{
    public class DomainEntity
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as DomainEntity;
            if (other == null) return false;
            return Equals(other);
        }

        protected bool Equals(DomainEntity other)
        {
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        protected void SetString(ref string field, string value)
        {
            if (value == null)
                field = string.Empty;
            else
                field = value;
        }
    }
}
