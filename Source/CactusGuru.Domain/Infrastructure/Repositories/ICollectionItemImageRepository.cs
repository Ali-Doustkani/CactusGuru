using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using System;
using System.Collections.Generic;

namespace CactusGuru.Domain.Persistance.Repositories
{
    public interface ICollectionItemImageRepository : IRepository<CollectionItemImage>
    {
        IEnumerable<CollectionItemImage> GetByRange(int startIndex, int count);
        IEnumerable<CollectionItemImage> GetByCollectionItemId(Guid collectionItemId);
        IEnumerable<Guid> GetIdsByCollectionItemId(Guid collectionItemId);
        void AddFullImage(Guid id, byte[] imageContent);
        void DeleteFullImage(Guid id);
        void UpdateSharedOnInstagram(IEnumerable<Guid> ids);
        byte[] GetFullImage(Guid id);
    }
}
