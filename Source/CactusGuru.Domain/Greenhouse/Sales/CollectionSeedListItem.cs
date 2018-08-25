using CactusGuru.Infrastructure.Utils;

namespace CactusGuru.Domain.Greenhouse.Sales
{
    public class CollectionSeedListItem : SeedListItemBase
    {
        public CollectionSeedListItem(CollectionItem collectionItem)
            : base(collectionItem.Code)
        {
            ArgumentChecker.CheckEmpty(collectionItem);
            CollectionItem = collectionItem;
        }

        public CollectionItem CollectionItem { get; set; }
    }
}
