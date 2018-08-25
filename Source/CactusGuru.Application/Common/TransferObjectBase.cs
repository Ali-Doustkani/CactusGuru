using System;

namespace CactusGuru.Application.Common
{
    public abstract class TransferObjectBase
    {
        public Guid Id { get; set; }

        public TransferObjectBase Clone()
        {
            return (TransferObjectBase)MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            var transferObjectBase = obj as TransferObjectBase;
            return transferObjectBase != null && Id.Equals(transferObjectBase.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
