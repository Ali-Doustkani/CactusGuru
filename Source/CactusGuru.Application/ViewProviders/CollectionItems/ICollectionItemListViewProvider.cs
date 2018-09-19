using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactusGuru.Application.ViewProviders.CollectionItems
{
    public interface ICollectionItemListViewProvider
    {
        void DeleteCollectionItem(Guid collectionItemId);
        Task<IEnumerable<CollectionItemDto>> GetCollectionItemsAsync(string sortBy);
        CollectionItemDto GetCollectionItem(Guid id);
        CollectionItemDto Convert(Common.CollectionItemDto dto);
        string GenerateName(Guid id);
    }
}
