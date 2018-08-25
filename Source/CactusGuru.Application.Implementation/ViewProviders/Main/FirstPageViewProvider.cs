using CactusGuru.Application.ViewProviders.Main;
using CactusGuru.Domain.Persistance.Repositories;

namespace CactusGuru.Application.Implementation.ViewProviders.Main
{
    public class FirstPageViewProvider : IFirstPageViewProvider
    {
        public FirstPageViewProvider(ICollectionItemRepository collectionItemRepo)
        {
            _collectionItemRepo = collectionItemRepo;
        }

        private readonly ICollectionItemRepository _collectionItemRepo;

        public int GetItemsCount()
        {
            return _collectionItemRepo.GetCount();
        }
    }
}
