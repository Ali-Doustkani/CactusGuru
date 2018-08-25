namespace CactusGuru.Domain.Greenhouse.Formatting
{
    public class TaxonFormatter : IFormatter<Taxon>
    {
        public TaxonFormatter(IFormatter<Genus> genusFormatter)
        {
            _genusFormatter = genusFormatter;
        }

        private readonly IFormatter<Genus> _genusFormatter;

        public string Format(Taxon taxon)
        {
            if (taxon.Genus.Equals(Genus.Empty))
                return string.Empty;
            if (string.IsNullOrEmpty(taxon.Genus.Title))
                return string.Empty;
            var taxonwithoutGenus = FormatTaxon(taxon);
            return $"{_genusFormatter.Format(taxon.Genus)} {taxonwithoutGenus}";
        }

        public string FormatTaxon(Taxon taxon)
        {
            var displayName = taxon.Species.Trim().ToLower();
            return ConcatToSubSpecies(taxon, displayName);
        }

        private string ConcatToSubSpecies(Taxon taxon, string displayName)
        {
            var sspVar = GetSubSpecies(taxon);
            if (!string.IsNullOrEmpty(sspVar))
                displayName = $"{displayName}{sspVar}";
            return displayName;
        }

        private string GetSubSpecies(Taxon taxon)
        {
            var ret = string.Empty;
            if (!string.IsNullOrEmpty(taxon.SubSpecies))
                ret = $"{ret} ssp. {taxon.SubSpecies}";
            if (!string.IsNullOrEmpty(taxon.Variety))
                ret = $"{ret} var. {taxon.Variety}";
            if (!string.IsNullOrEmpty(taxon.Forma))
                ret = $"{ret} fa. {taxon.Forma}";
            if (!string.IsNullOrEmpty(taxon.Cultivar))
                ret = $"{ret} cv. {GenusFirstLetterCapitalFormatter.CapitalFirstChar(taxon.Cultivar)}";
            return ret;
        }
    }
}
