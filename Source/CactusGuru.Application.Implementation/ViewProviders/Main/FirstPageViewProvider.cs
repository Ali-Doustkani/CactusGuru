using CactusGuru.Application.ViewProviders.Main;
using CactusGuru.Domain.Persistance.Repositories;

namespace CactusGuru.Application.Implementation.ViewProviders.Main
{
    public class FirstPageViewProvider : ViewProviderBase, IFirstPageViewProvider
    {
        public int GetItemsCount()
        {
            return Get<ICollectionItemRepository>().GetCount();
        }
    }
}
