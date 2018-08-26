using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Domain.Greenhouse;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.LabelPrinting
{
    public class TaxonAssembler : AssemblerBase<Taxon, TaxonDto>
    {
        protected override void FillDataTransferEntityImp(TaxonDto dto, Taxon domainEntity)
        {
            dto.Id = domainEntity.Id;
            dto.Genus = domainEntity.Genus.ToString("GENUS");
            dto.Species = domainEntity.ToString("{taxon}");
            dto.Name = domainEntity.ToString("{GENUS} {taxon}");
        }

        protected override void FillDomainEntityImp(Taxon domainEntity, TaxonDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
