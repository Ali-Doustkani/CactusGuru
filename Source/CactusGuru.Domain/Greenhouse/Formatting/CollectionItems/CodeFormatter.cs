namespace CactusGuru.Domain.Greenhouse.Formatting.CollectionItems
{
    public class CodeFormatter : IFormatter<CollectionItem>
    {
        public CodeFormatter(IFormatter<CollectionItem> formatter)
        {
            _formatter = formatter;
        }

        private readonly IFormatter<CollectionItem> _formatter;

        public string Format(CollectionItem domainEntity)
        {
            return $"{domainEntity.Code} - {_formatter.Format(domainEntity)}";
        }
    }
}
