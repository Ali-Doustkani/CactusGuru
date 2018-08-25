namespace CactusGuru.Domain.Greenhouse.Formatting.CollectionItems
{
    public class TaxonFieldFormatter : IFormatter<CollectionItem>
    {
        public TaxonFieldFormatter(IFormatter<Taxon> taxonFormatter)
        {
            _taxonFormatter = taxonFormatter;
        }

        private readonly IFormatter<Taxon> _taxonFormatter;

        public string Format(CollectionItem item)
        {
            if (item.Taxon == null) return string.Empty;
            var taxon = _taxonFormatter.Format(item.Taxon);
            var field = CollectionItemFormatter.FormatField(item);
            return $"{taxon} {field}".Trim();
        }
    }
}
