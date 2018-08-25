using CactusGuru.Application.Common;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;

namespace CactusGuru.Application.Implementation.Assemblers
{
    public class CollectorAssembler : AssemblerBase<Collector, CollectorDto>
    {
        public CollectorAssembler(IFormatter<Collector> formatter)
        {
            _formatter = formatter;
        }

        private readonly IFormatter<Collector> _formatter;

        protected override void FillDataTransferEntityImp(CollectorDto dto, Collector entity)
        {
            dto.Acronym = entity.Acronym;
            dto.FullName = entity.FullName;
            dto.FormattedName = _formatter.Format(entity);
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
