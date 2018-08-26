namespace CactusGuru.Domain.Greenhouse.Formatting.CollectionItems
{
    public class LabelFormatter : IFormatter<CollectionItem>
    {
        public string Format(CollectionItem collectionItem)
        {
            if (HasField(collectionItem))
                return $"{ collectionItem.Taxon.ToString("{GENUS} {taxon}")} {GetField(collectionItem)}";
            return collectionItem.Taxon.ToString("{GENUS} {taxon}");
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
