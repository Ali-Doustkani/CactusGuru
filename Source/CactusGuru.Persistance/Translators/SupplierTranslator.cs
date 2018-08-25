using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;

namespace CactusGuru.Persistance.Translators
{
    public class SupplierTranslator : TranslatorBase<Supplier, tblSupplier>
    {
        public override void FillDataEntity(tblSupplier dataEntity, Supplier domainEntity)
        {
            dataEntity.Acronym = domainEntity.Acronym;
            dataEntity.Id = domainEntity.Id;
            dataEntity.Title = domainEntity.FullName;
            dataEntity.WebSite = domainEntity.WebSite;
        }

        public override Supplier ToDomainEntity(tblSupplier dataEntity)
        {
            if (dataEntity == null)
                return Supplier.Empty;
            var ret = new Supplier();
            ret.Acronym = dataEntity.Acronym;
            ret.FullName = dataEntity.Title;
            ret.Id = dataEntity.Id;
            ret.WebSite = dataEntity.WebSite;
            return ret;
        }
    }
}
