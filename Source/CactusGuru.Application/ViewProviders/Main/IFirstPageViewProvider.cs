using System.Threading.Tasks;

namespace CactusGuru.Application.ViewProviders.Main
{
    public interface IFirstPageViewProvider
    {
        Task<int> GetItemsCount();
    }
}
