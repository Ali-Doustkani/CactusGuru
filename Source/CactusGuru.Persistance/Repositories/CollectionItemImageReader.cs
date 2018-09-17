using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;
using System.Linq;

namespace CactusGuru.Persistance.Repositories
{
    internal class CollectionItemImageReader : IReader<CollectionItemImage>
    {
        public CollectionItemImageReader(CactusGuruEntities context, TranslatorBase<CollectionItemImage, tblCollectionItemImage> translator)
        {
            _context = context;
            _translator = translator;
        }

        private readonly CactusGuruEntities _context;
        private readonly TranslatorBase<CollectionItemImage, tblCollectionItemImage> _translator;
        private int _skiped;

        public CollectionItemImage Value { get; private set; }

        public bool Read()
        {
            var entity = _context.tblCollectionItemImage
                .Where(x => !x.SharedOnInstagram)
                .OrderByDescending(x => x.DateAdded)
                .Skip(_skiped)
                .Take(1)
                .SingleOrDefault();
            if (entity == null)
                return false;
            _skiped++;
            Value = _translator.ToDomainEntity(entity);
            return true;
        }
    }
}
