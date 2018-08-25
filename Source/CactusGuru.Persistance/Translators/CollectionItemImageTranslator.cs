using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;

namespace CactusGuru.Persistance.Translators
{
    public class CollectionItemImageTranslator : TranslatorBase<CollectionItemImage, tblCollectionItemImage>
    {
        public override void FillDataEntity(tblCollectionItemImage entity, CollectionItemImage poco)
        {
            entity.Id = poco.Id;
            entity.tblCollectionItemId = poco.CollectionItemId;
            entity.DateAdded = poco.DateAdded;
            entity.Description = poco.Description;
            entity.Image = poco.Thumbnail;
            entity.SharedOnInstagram = poco.SharedOnInstagram;
        }

        public override CollectionItemImage ToDomainEntity(tblCollectionItemImage entity)
        {
            var ret = new CollectionItemImage();
            ret.Thumbnail = entity.Image;
            ret.DateAdded = entity.DateAdded;
            ret.Description = entity.Description;
            ret.Id = entity.Id;
            ret.CollectionItemId = entity.tblCollectionItemId;
            ret.SharedOnInstagram = entity.SharedOnInstagram;
            return ret;
        }
    }
}
