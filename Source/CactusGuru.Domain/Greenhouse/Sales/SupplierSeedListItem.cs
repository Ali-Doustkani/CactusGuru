using CactusGuru.Infrastructure.Utils;

namespace CactusGuru.Domain.Greenhouse.Sales
{
    public class SupplierSeedListItem : SeedListItemBase
    {
        public SupplierSeedListItem(string code, Taxon taxon)
            : base(code)
        {
            ArgumentChecker.CheckEmpty(taxon);
            Taxon = taxon;
        }

        public Taxon Taxon { get; set; }

        public Supplier Supplier { get; set; }

        public string SupplierCode { get; set; }
    }
}
