using CactusGuru.Infrastructure.ObjectCreation;
using System;

namespace CactusGuru.Domain.Greenhouse.Factories
{
    public class CollectionItemImageFactoryArg : FactoryArg
    {
        public CollectionItemImageFactoryArg(Guid collectionItemId, string filePath)
        {
            CollectionItemId = collectionItemId;
            FilePath = filePath;
        }

        public Guid CollectionItemId { get; }
        public string FilePath { get; }
    }
}
