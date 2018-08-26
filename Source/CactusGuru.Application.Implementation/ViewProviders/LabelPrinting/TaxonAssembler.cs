using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.LabelPrinting
{
    public class TaxonAssembler : AssemblerBase<Taxon, TaxonDto>
    {
        public TaxonAssembler(
            IFormatter<Taxon> speciesFormatter,
            IFormatter<Taxon> taxonFormatter)
        {
            _speciesFormatter = speciesFormatter;
            _taxonFormatter = taxonFormatter;
        }

        private readonly IFormatter<Taxon> _speciesFormatter;
        private readonly IFormatter<Taxon> _taxonFormatter;

        protected override void FillDataTransferEntityImp(TaxonDto dto, Taxon domainEntity)
        {
            dto.Id = domainEntity.Id;
            dto.Genus = domainEntity.Genus.ToString("GENUS");
            dto.Species = _speciesFormatter.Format(domainEntity);
            dto.Name = _taxonFormatter.Format(domainEntity);
        }

        protected override void FillDomainEntityImp(Taxon domainEntity, TaxonDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
