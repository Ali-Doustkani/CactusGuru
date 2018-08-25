using CactusGuru.Application.Common;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;

namespace CactusGuru.Application.Implementation.Assemblers
{
    public class GenusAssembler : AssemblerBase<Genus, GenusDto>
    {
        public GenusAssembler(IFormatter<Genus> formatter)
        {
            _formatter = formatter;
        }

        private readonly IFormatter<Genus> _formatter;

        protected override void FillDataTransferEntityImp(GenusDto transferEntity, Genus domainEntity)
        {
            transferEntity.Name = _formatter.Format(domainEntity);
        }

        protected override void FillDomainEntityImp(Genus domainEntity, GenusDto dataTransferentity)
        {
            domainEntity.Title = dataTransferentity.Name;
        }
    }
}
