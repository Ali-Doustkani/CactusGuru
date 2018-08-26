namespace CactusGuru.Domain.Greenhouse.Formatting.CollectionItems
{
    public class TaxonFieldFormatter : IFormatter<CollectionItem>
    {
        public string Format(CollectionItem item)
        {
            if (item.Taxon == null) return string.Empty;
            var taxon = item.Taxon.ToString("{GENUS} {taxon}");
            var field = CollectionItemFormatter.FormatField(item);
            return $"{taxon} {field}".Trim();
        }
    }
}
