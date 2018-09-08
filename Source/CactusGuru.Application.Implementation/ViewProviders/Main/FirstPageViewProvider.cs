using CactusGuru.Application.ViewProviders.Main;
using CactusGuru.Domain.Persistance.Repositories;
using System.Threading.Tasks;

namespace CactusGuru.Application.Implementation.ViewProviders.Main
{
    public class FirstPageViewProvider : ViewProviderBase, IFirstPageViewProvider
    {
        public Task<int> GetItemsCount()
        {
            return Task.Factory.StartNew(() =>
             {
                 using (var locator = Begin())
                     return locator.Get<ICollectionItemRepository>().GetCount();
             });
        }
    }
}
