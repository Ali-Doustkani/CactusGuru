using CactusGuru.Application.Common;

namespace CactusGuru.Application.ViewProviders
{
    public interface ISupplierViewProvider : IDataEntryViewProvider
    {
        bool HasSimilar(SupplierDto supplierDto);
    }
}
