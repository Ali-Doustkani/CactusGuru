using System.Collections.Generic;
using CactusGuru.Application.Common;
using CactusGuru.Infrastructure;

namespace CactusGuru.Application.Implementation
{
    public abstract class AssemblerBase<TDomainEntity, TDataTransferEntity>
        where TDataTransferEntity : TransferObjectBase, new()
        where TDomainEntity : DomainEntity
    {
        public IEnumerable<TDataTransferEntity> ToDataTransferEntities(IEnumerable<TDomainEntity> domainEntities)
        {
            var ret = new List<TDataTransferEntity>();
            foreach (var entity in domainEntities)
                ret.Add(ToDataTransferEntity(entity));
            return ret;
        }

        public TDataTransferEntity ToDataTransferEntity(TDomainEntity domainEntity)
        {
            var ret = new TDataTransferEntity();
            FillDataTransferEntity(ret, domainEntity);
            return ret;
        }

        public void FillDataTransferEntity(TDataTransferEntity transferEntity, TDomainEntity domainEntity)
        {
            transferEntity.Id = domainEntity.Id;
            FillDataTransferEntityImp(transferEntity, domainEntity);
        }

        protected abstract void FillDataTransferEntityImp(TDataTransferEntity dto, TDomainEntity domainEntity);

        public void FillDomainEntity(TDomainEntity domainEntity, TDataTransferEntity dataTransferentity)
        {
            domainEntity.Id = dataTransferentity.Id;
            FillDomainEntityImp(domainEntity, dataTransferentity);
        }

        protected abstract void FillDomainEntityImp(TDomainEntity domainEntity, TDataTransferEntity dto);
    }
}
