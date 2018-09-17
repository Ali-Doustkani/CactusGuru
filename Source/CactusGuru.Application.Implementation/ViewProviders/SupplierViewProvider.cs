using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class SupplierViewProvider : CommonDataEntryViewProvider<Supplier, SupplierDto>, ISupplierViewProvider
    {
        public bool HasSimilar(SupplierDto supplierDto)
        {
            using (var locator = Begin())
            {
                var supplier = new Supplier
                {
                    Acronym = supplierDto.Acronym,
                    FullName = supplierDto.FullName,
                    Id = supplierDto.Id,
                    WebSite = supplierDto.Website
                };
                return locator.Get<ISupplierRepository>().HasSimilar(supplier);
            }
        }

        protected override object GetOrderKey(SupplierDto dto) => dto.FullName;
    }
}
