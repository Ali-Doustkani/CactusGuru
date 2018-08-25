using System.Collections.Generic;

namespace CactusGuru.Infrastructure.Persistance
{
    public abstract class TranslatorBase<TDomainEntity, TDataEntity>
     where TDataEntity : new()
    {
        public TDataEntity ToDataEntity(TDomainEntity domainEntity)
        {
            var ret = new TDataEntity();
            FillDataEntity(ret, domainEntity);
            return ret;
        }

        public IEnumerable<TDomainEntity> ToDomainEntities(IEnumerable<TDataEntity> dataEntities)
        {
            var ret = new List<TDomainEntity>();
            foreach (var entity in dataEntities)
                ret.Add(ToDomainEntity(entity));
            return ret;
        }

        public abstract void FillDataEntity(TDataEntity dataEntity, TDomainEntity domainEntity);

        public abstract TDomainEntity ToDomainEntity(TDataEntity dataEntity);
    }
}
