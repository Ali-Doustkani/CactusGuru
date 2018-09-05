using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;

namespace CactusGuru.Persistance.Translators
{
    public class TaxonTranslator : TranslatorBase<Taxon, tblTaxon>
    {
        public TaxonTranslator(TranslatorBase<Genus, tblGenus> genusTranslator)
        {
            ArgumentChecker.CheckNull(genusTranslator);
            _genusTranslator = genusTranslator;
        }

        private readonly TranslatorBase<Genus, tblGenus> _genusTranslator;

        public override void FillDataEntity(tblTaxon dataEntity, Taxon domainEntity)
        {
            dataEntity.Cultivar = domainEntity.Cultivar;
            dataEntity.tblGenusId = domainEntity.Genus.Id;
            dataEntity.Id = domainEntity.Id;
            dataEntity.Species = domainEntity.Species;
            dataEntity.SubSpecies = domainEntity.SubSpecies;
            dataEntity.Variety = domainEntity.Variety;
            dataEntity.Forma = domainEntity.Forma;
        }

        public override Taxon ToDomainEntity(tblTaxon dataEntity)
        {
            var ret = new Taxon();
            ret.Cultivar = dataEntity.Cultivar;
            ret.Genus = _genusTranslator.ToDomainEntity(dataEntity.tblGenus);
            ret.Id = dataEntity.Id;
            ret.Species = dataEntity.Species;
            ret.SubSpecies = dataEntity.SubSpecies;
            ret.Variety = dataEntity.Variety;
            ret.Forma = dataEntity.Forma;
            return ret;
        }
    }
}
