using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using System;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders.CollectionItemList
{
    public class CollectionItemListViewProvider : ICollectionItemListViewProvider
    {
        public CollectionItemListViewProvider(ITerminator<CollectionItem> terminator,
           ICollectionItemRepository repo,
           AssemblerBase<CollectionItem, CollectionItemDto> assembler)
        {
            _terminator = terminator;
            _repo = repo;
            _assembler = assembler;
        }

        private readonly ITerminator<CollectionItem> _terminator;
        private readonly ICollectionItemRepository _repo;
        private readonly AssemblerBase<CollectionItem, CollectionItemDto> _assembler;
        private int _start;

        public void DeleteCollectionItem(Guid collectionItemId)
        {
            _terminator.Terminate(collectionItemId);
        }

        public CollectionItemDto GetCollectionItem(Guid id)
        {
            return _assembler.ToDataTransferEntity(_repo.Get(id));
        }

        public bool GetCollectionItemsAsync(Action<CollectionItemAsyncDto> callback)
        {
            var items = _repo.GetByRange(_start, 10);
            var dtos = _assembler.ToDataTransferEntities(items);
            if (dtos.Any())
            {
                callback(new CollectionItemAsyncDto(dtos));
                _start += 10;
                return true;
            }
            _start = 0;
            return false;
        }
    }
}
