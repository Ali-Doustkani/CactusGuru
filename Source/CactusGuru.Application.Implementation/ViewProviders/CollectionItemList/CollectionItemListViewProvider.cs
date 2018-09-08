using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using System;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders.CollectionItemList
{
    public class CollectionItemListViewProvider : ViewProviderBase, ICollectionItemListViewProvider
    {
        private int _start;

        public void DeleteCollectionItem(Guid collectionItemId)
        {
            Get<ITerminator<CollectionItem>>().Terminate(collectionItemId);
        }

        public CollectionItemDto GetCollectionItem(Guid id)
        {
            var repo = Get<ICollectionItemRepository>();
            var assembler = Get<AssemblerBase<CollectionItem, CollectionItemDto>>();
            return assembler.ToDataTransferEntity(repo.Get(id));
        }

        public bool GetCollectionItemsAsync(Action<CollectionItemAsyncDto> callback)
        {
            var repo = Get<ICollectionItemRepository>();
            var assembler = Get<AssemblerBase<CollectionItem, CollectionItemDto>>();
            var items = repo.GetByRange(_start, 10);
            var dtos = assembler.ToDataTransferEntities(items);
            if (dtos.Any())
            {
                callback(new CollectionItemAsyncDto(dtos));
                _start += 10;
                return true;
            }
            _start = 0;
            return false;
        }

        public CollectionItemDto Convert(Common.CollectionItemDto dto)
        {
            var repo = Get<ICollectionItemRepository>();
            var assembler = Get<AssemblerBase<CollectionItem, CollectionItemDto>>();
            return assembler.ToDataTransferEntity(repo.Get(dto.Id));
        }
    }
}
