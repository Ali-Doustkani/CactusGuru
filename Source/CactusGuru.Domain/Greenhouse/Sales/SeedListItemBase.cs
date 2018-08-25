using System;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Utils;

namespace CactusGuru.Domain.Greenhouse.Sales
{
    public class SeedListItemBase : DomainEntity
    {
        public SeedListItemBase(string code)
        {
            ArgumentChecker.CheckEmpty(code);
            Code = code;
        }

        public Guid SeedListId { get; set; }

        public string Code { get; private set; }

        public int StandardPocketCount { get; set; }

        public int StandardPocketPrice { get; set; }

        public int? Pocket100sPrice { get; set; }

        public int? Pocket500sPrice { get; set; }

        public int? Pocket1000sPrice { get; set; }

        public string Description { get; set; }
    }
}
