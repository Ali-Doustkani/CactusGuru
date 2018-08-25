namespace CactusGuru.Domain.Greenhouse.Formatting.CollectionItems
{
    public class CollectionItemFormatter : IFormatter<CollectionItem>
    {
        public CollectionItemFormatter(IFormatter<Taxon> taxonFormatter)
        {
            _taxonFormatter = taxonFormatter;
        }

        private readonly IFormatter<Taxon> _taxonFormatter;

        public string Format(CollectionItem collectionItem)
        {
            if (collectionItem.Taxon == null) return string.Empty;
            var taxon = _taxonFormatter.Format(collectionItem.Taxon);
            var field = FormatField(collectionItem);
            string displayName;
            if (HasLocality(collectionItem))
                displayName = $"{taxon} {field}";
            else
                displayName = $"{taxon} {field}, {collectionItem.Locality}";
            return displayName.Trim();
        }

        private bool HasLocality(CollectionItem collectionItem)
        {
            return string.IsNullOrEmpty(collectionItem.Locality);
        }

        public static string FormatField(CollectionItem collectionItem)
        {
            if (collectionItem.Collector == null) return string.Empty;
            return collectionItem.Collector.Acronym + collectionItem.FieldNumber;
        }
    }
}
