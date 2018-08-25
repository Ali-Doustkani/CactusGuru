namespace CactusGuru.Domain.Greenhouse.Formatting.CollectionItems
{
    public class LabelFormatter : IFormatter<CollectionItem>
    {
        public LabelFormatter(IFormatter<Taxon> taxonFormatter)
        {
            _taxonFormatter = taxonFormatter;
        }

        private readonly IFormatter<Taxon> _taxonFormatter;

        public string Format(CollectionItem collectionItem)
        {
            if (HasField(collectionItem))
                return $"{_taxonFormatter.Format(collectionItem.Taxon)} {GetField(collectionItem)}";
            return _taxonFormatter.Format(collectionItem.Taxon);
        }

        private string GetField(CollectionItem collectionItem)
        {
            return $"{collectionItem.Collector.Acronym} {collectionItem.FieldNumber}";
        }

        private bool HasField(CollectionItem collectionItem)
        {
            return !string.IsNullOrEmpty(collectionItem.FieldNumber);
        }
    }
}
