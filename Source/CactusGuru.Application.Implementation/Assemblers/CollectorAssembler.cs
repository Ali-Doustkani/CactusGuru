using CactusGuru.Application.Common;
using CactusGuru.Domain.Greenhouse;

namespace CactusGuru.Application.Implementation.Assemblers
{
    public class CollectorAssembler : AssemblerBase<Collector, CollectorDto>
    {
        protected override void FillDataTransferEntityImp(CollectorDto dto, Collector entity)
        {
            dto.Acronym = entity.Acronym;
            dto.FullName = entity.FullName;
            dto.FormattedName = entity.ToString();
            dto.Website = entity.WebSite;
        }

        protected override void FillDomainEntityImp(Collector domainEntity, CollectorDto dto)
        {
            domainEntity.Acronym = dto.Acronym;
            domainEntity.FullName = dto.FullName;
            domainEntity.WebSite = dto.Website;
        }
    }
}
