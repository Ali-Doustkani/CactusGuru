using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;

namespace CactusGuru.Persistance.Translators
{
    public class CollectorTranslator : TranslatorBase<Collector, tblCollector>
    {
        public override void FillDataEntity(tblCollector dataEntity, Collector domainEntity)
        {
            dataEntity.FieldAcronym = domainEntity.Acronym;
            dataEntity.Id = domainEntity.Id;
            dataEntity.Title = domainEntity.FullName;
            dataEntity.WebSite = domainEntity.WebSite;
        }

        public override Collector ToDomainEntity(tblCollector entity)
        {
            if (entity == null)
                return Collector.Empty;
            var ret = new Collector();
            ret.Acronym = entity.FieldAcronym;
            ret.FullName = entity.Title;
            ret.Id = entity.Id;
            ret.WebSite = entity.WebSite;
            return ret;
        }
    }
}
