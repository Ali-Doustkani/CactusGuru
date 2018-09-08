﻿using CactusGuru.Application.ViewProviders.CollectionItems;
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
            using (var locator = Begin())
            {
                locator.Get<Terminator<CollectionItem>>().Terminate(collectionItemId);
            }
        }

        public CollectionItemDto GetCollectionItem(Guid id)
        {
            using (var locator = Begin())
            {
                var repo = locator.Get<ICollectionItemRepository>();
                var assembler = locator.Get<AssemblerBase<CollectionItem, CollectionItemDto>>();
                return assembler.ToDataTransferEntity(repo.Get(id));
            }
        }

        public bool GetCollectionItemsAsync(Action<CollectionItemAsyncDto> callback)
        {
            using (var locator = Begin())
            {
                var repo = locator.Get<ICollectionItemRepository>();
                var assembler = locator.Get<AssemblerBase<CollectionItem, CollectionItemDto>>();
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
        }

        public CollectionItemDto Convert(Common.CollectionItemDto dto)
        {
            using (var locator = Begin())
            {
                var repo = locator.Get<ICollectionItemRepository>();
                var assembler = locator.Get<AssemblerBase<CollectionItem, CollectionItemDto>>();
                return assembler.ToDataTransferEntity(repo.Get(dto.Id));
            }
        }
    }
}
