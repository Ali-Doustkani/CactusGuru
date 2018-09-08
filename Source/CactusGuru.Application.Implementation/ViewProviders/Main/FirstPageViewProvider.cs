using CactusGuru.Application.ViewProviders.Main;
using CactusGuru.Domain.Persistance.Repositories;

namespace CactusGuru.Application.Implementation.ViewProviders.Main
{
    public class FirstPageViewProvider : ViewProviderBase, IFirstPageViewProvider
    {
        public int GetItemsCount()
        {
            using (var locator = Begin())
                return locator.Get<ICollectionItemRepository>().GetCount();
        }
    }
}
