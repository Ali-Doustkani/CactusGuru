using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactusGuru.Application.ViewProviders.CollectionItems
{
    public interface ICollectionItemListViewProvider
    {
        void DeleteCollectionItem(Guid collectionItemId);
        Task<IEnumerable<CollectionItemDto>> GetCollectionItemsAsync();
        CollectionItemDto GetCollectionItem(Guid id);
        CollectionItemDto Convert(Common.CollectionItemDto dto);
    }
}
