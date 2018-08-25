using System;
using System.Collections.Generic;

namespace CactusGuru.Application.ViewProviders.CollectionItems
{
    public interface ICollectionItemListViewProvider
    {
        void DeleteCollectionItem(Guid collectionItemId);
        bool GetCollectionItemsAsync(Action<CollectionItemAsyncDto> callback);
        CollectionItemDto GetCollectionItem(Guid id);
    }
}
