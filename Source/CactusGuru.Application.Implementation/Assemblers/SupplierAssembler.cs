using CactusGuru.Application.Common;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;

namespace CactusGuru.Application.Implementation.Assemblers
{
    public class SupplierAssembler : AssemblerBase<Supplier, SupplierDto>
    {
        public SupplierAssembler(IFormatter<Supplier> formatter)
        {
            _formatter = formatter;
        }

        private readonly IFormatter<Supplier> _formatter;

        protected override void FillDataTransferEntityImp(SupplierDto dto, Supplier domainEntity)
        {
            dto.Acronym = domainEntity.Acronym;
            dto.FullName = domainEntity.FullName;
            dto.FormattedName = _formatter.Format(domainEntity);
            dto.Website = domainEntity.WebSite;
        }

        protected override void FillDomainEntityImp(Supplier domainEntity, SupplierDto dto)
        {
            domainEntity.FullName = dto.FullName;
            domainEntity.Acronym = dto.Acronym;
            domainEntity.WebSite = dto.Website;
        }
    }
}
