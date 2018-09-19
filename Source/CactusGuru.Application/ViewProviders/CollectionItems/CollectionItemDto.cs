using CactusGuru.Application.Common;
using System;

namespace CactusGuru.Application.ViewProviders.CollectionItems
{
    public class CollectionItemDto : TransferObjectBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public Guid TaxonId { get; set; }
        public Guid GenusId { get; set; }
    }
}
