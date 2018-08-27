using CactusGuru.Application.Common;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System;

namespace CactusGuru.Application.Implementation.Assemblers
{
    public class TaxonAssembler : AssemblerBase<Taxon, TaxonDto>
    {
        public TaxonAssembler(
            AssemblerBase<Genus, GenusDto> genusAssembler,
            IGenusRepository genusRepository)
        {
            _genusAssembler = genusAssembler;
            _genusRepository = genusRepository;
        }

        private readonly AssemblerBase<Genus, GenusDto> _genusAssembler;
        private readonly IGenusRepository _genusRepository;

        protected override void FillDataTransferEntityImp(TaxonDto transferEntity, Taxon domainEntity)
        {
            transferEntity.Name = domainEntity.Format("{GENUS} {taxon}");
            transferEntity.Cultivar = domainEntity.Cultivar;
            transferEntity.Forma = domainEntity.Forma;
            transferEntity.Genus = _genusAssembler.ToDataTransferEntity(domainEntity.Genus);
            transferEntity.Species = domainEntity.Species;
            transferEntity.SubSpecies = domainEntity.SubSpecies;
            transferEntity.Variety = domainEntity.Variety;
        }

        protected override void FillDomainEntityImp(Taxon domainEntity, TaxonDto dataTransferentity)
        {
            if (dataTransferentity.Genus == null)
                throw new AssembleException(nameof(TaxonDto.Genus));

            domainEntity.Cultivar = dataTransferentity.Cultivar;
            domainEntity.Forma = dataTransferentity.Forma;
            domainEntity.Genus = GetGenus(dataTransferentity);
            domainEntity.Species = dataTransferentity.Species;
            domainEntity.SubSpecies = dataTransferentity.SubSpecies;
            domainEntity.Variety = dataTransferentity.Variety;
        }

        private Genus GetGenus(TaxonDto dataTransferentity)
        {
            if (dataTransferentity.Genus.Id.Equals(Guid.Empty))
                return null;
            return _genusRepository.Get(dataTransferentity.Genus.Id);
        }
    }
}
