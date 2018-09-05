using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders
{
    public interface IGenusViewProvider : IDataEntryViewProvider
    {
        bool HasSimilar(GenusDto genusDto);
    }
}
