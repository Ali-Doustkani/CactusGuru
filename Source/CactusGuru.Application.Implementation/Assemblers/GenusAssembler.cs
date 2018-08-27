using CactusGuru.Application.Common;
using CactusGuru.Domain.Greenhouse;

namespace CactusGuru.Application.Implementation.Assemblers
{
    public class GenusAssembler : AssemblerBase<Genus, GenusDto>
    {
        protected override void FillDataTransferEntityImp(GenusDto transferEntity, Genus domainEntity)
        {
            transferEntity.Name = domainEntity.Format("Genus");
        }

        protected override void FillDomainEntityImp(Genus domainEntity, GenusDto dataTransferentity)
        {
            domainEntity.Title = dataTransferentity.Name;
        }
    }
}
