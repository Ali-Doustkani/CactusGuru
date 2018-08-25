using System.Collections.Generic;

namespace CactusGuru.Application.ViewProviders.CollectionItems
{
    public class CollectionItemAsyncDto
    {
        public CollectionItemAsyncDto(IEnumerable<CollectionItemDto> loadedItems)
        {
            LoadedItems = loadedItems;
        }

        public IEnumerable<CollectionItemDto> LoadedItems { get; }
    }
}
