using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders
{
    public interface ICollectorViewProvider : IDataEntryViewProvider
    {
        bool HasSimilar(CollectorDto dto);
    }
}
