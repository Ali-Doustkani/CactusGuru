using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.LabelPrinting
{
    public class TaxonAssembler : AssemblerBase<Taxon, TaxonDto>
    {
        public TaxonAssembler(IFormatter<Genus> genusFormatter,
            IFormatter<Taxon> speciesFormatter,
            IFormatter<Taxon> taxonFormatter)
        {
            _genusFormatter = genusFormatter;
            _speciesFormatter = speciesFormatter;
            _taxonFormatter = taxonFormatter;
        }

        private readonly IFormatter<Genus> _genusFormatter;
        private readonly IFormatter<Taxon> _speciesFormatter;
        private readonly IFormatter<Taxon> _taxonFormatter;

        protected override void FillDataTransferEntityImp(TaxonDto dto, Taxon domainEntity)
        {
            dto.Id = domainEntity.Id;
            dto.Genus = _genusFormatter.Format(domainEntity.Genus);
            dto.Species = _speciesFormatter.Format(domainEntity);
            dto.Name = _taxonFormatter.Format(domainEntity);
        }

        protected override void FillDomainEntityImp(Taxon domainEntity, TaxonDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
