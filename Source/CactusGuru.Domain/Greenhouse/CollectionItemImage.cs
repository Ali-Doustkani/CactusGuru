using System;
using CactusGuru.Infrastructure;

namespace CactusGuru.Domain.Greenhouse
{
    public class CollectionItemImage : DomainEntity
    {
        public Guid CollectionItemId { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public byte[] Thumbnail { get; set; }
        public bool SharedOnInstagram { get; set; }
    }
}
