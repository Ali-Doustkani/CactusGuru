using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;

namespace CactusGuru.Persistance.Translators
{
    public class GenusTranslator : TranslatorBase<Genus, tblGenus>
    {
        public GenusTranslator(IFactory<Genus> factory)
        {
            ArgumentChecker.CheckNull(factory);
            _factory = factory;
        }

        private readonly IFactory<Genus> _factory;

        public override void FillDataEntity(tblGenus dataEntity, Genus domainEntity)
        {
            dataEntity.Id = domainEntity.Id;
            dataEntity.Name = domainEntity.Title;
        }

        public override Genus ToDomainEntity(tblGenus dataEntity)
        {
            var ret = _factory.CreateNew();
            ret.Id = dataEntity.Id;
            ret.Title = dataEntity.Name;
            return ret;
        }
    }
}
