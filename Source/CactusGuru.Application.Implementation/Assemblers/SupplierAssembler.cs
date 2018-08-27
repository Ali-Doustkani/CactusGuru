using CactusGuru.Application.Common;
using CactusGuru.Domain.Greenhouse;

namespace CactusGuru.Application.Implementation.Assemblers
{
    public class SupplierAssembler : AssemblerBase<Supplier, SupplierDto>
    {
        protected override void FillDataTransferEntityImp(SupplierDto dto, Supplier domainEntity)
        {
            dto.Acronym = domainEntity.Acronym;
            dto.FullName = domainEntity.FullName;
            dto.FormattedName = domainEntity.ToString();
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
