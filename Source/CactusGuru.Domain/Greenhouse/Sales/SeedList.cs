using System;
using System.Collections.Generic;
using System.Linq;
using CactusGuru.Infrastructure;

namespace CactusGuru.Domain.Greenhouse.Sales
{
    public class SeedList : DomainEntity
    {
        public SeedList()
        {
            _items = new List<SeedListItemBase>();
        }

        public string Name { get; set; }

        public DateTime PublishDate { get; set; }

        private readonly List<SeedListItemBase> _items;
        public IEnumerable<SeedListItemBase> Items
        {
            get { return _items.ToArray(); }
        }

        public void AddItem(SeedListItemBase item)
        {
            item.SeedListId = Id;
            _items.Add(item);
        }

        public void RemoveItem(SeedListItemBase item)
        {
            _items.Remove(item);
            item.Id = Guid.Empty;
        }

        public void ClearItems()
        {
            _items.Clear();
        }

        public string GenerateSupplierItemCode()
        {
            var intCode = _items.OfType<SupplierSeedListItem>().Count() + 1;
            return string.Format("N{0}", intCode);
        }
    }
}
