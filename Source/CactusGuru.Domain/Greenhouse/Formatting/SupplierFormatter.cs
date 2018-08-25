namespace CactusGuru.Domain.Greenhouse.Formatting
{
    public class SupplierFormatter : IFormatter<Supplier>
    {
        public string Format(Supplier supplier)
        {
            return AcronymFormatter.Format(supplier.FullName, supplier.Acronym);
        }
    }
}
