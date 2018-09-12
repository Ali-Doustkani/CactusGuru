using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CactusGuru.Application.Implementation.ViewProviders.CollectionItemList
{
    public class CollectionItemListViewProvider : ViewProviderBase, ICollectionItemListViewProvider
    {
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

        public Task<IEnumerable<CollectionItemDto>> GetCollectionItemsAsync(string sortBy)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var locator = Begin())
                {
                    var entities = Enumerable.Empty<CollectionItem>();
                    if (sortBy == "Genus")
                        entities = locator.Get<ICollectionItemRepository>().GetAllSortedByGenus();
                    else if (sortBy == "Code")
                        entities = locator.Get<ICollectionItemRepository>().GetAllSortedByCode();
                    return locator.Get<AssemblerBase<CollectionItem, CollectionItemDto>>().ToDataTransferEntities(entities);
                }
            });
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