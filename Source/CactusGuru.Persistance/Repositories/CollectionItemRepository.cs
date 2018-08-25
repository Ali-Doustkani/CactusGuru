using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Persistance.Repositories
{
    public class CollectionItemRepository : ICollectionItemRepository
    {
        public CollectionItemRepository(CactusGuruEntities context, TranslatorBase<CollectionItem, tblCollectionItem> translator)
        {
            _context = context;
            _translator = translator;
        }

        private readonly CactusGuruEntities _context;
        private readonly TranslatorBase<CollectionItem, tblCollectionItem> _translator;

        public void Add(CollectionItem collectionItem)
        {
            var entity = _translator.ToDataEntity(collectionItem);
            _context.tblCollectionItem.Add(entity);
        }

        public void Update(CollectionItem collectionItem)
        {
            var entity = _context.tblCollectionItem.Find(collectionItem.Id);
            _translator.FillDataEntity(entity, collectionItem);
        }

        public void Delete(Guid id)
        {
            var entity = _context.tblCollectionItem.Find(id);
            _context.tblCollectionItem.Remove(entity);
        }

        public CollectionItem Get(Guid id)
        {
            var entity = _context.tblCollectionItem.AsNoTracking().Single(x => x.Id == id);
            return _translator.ToDomainEntity(entity);
        }

        public IEnumerable<CollectionItem> GetAll()
        {
            return _translator.ToDomainEntities(
                _context.tblCollectionItem.
                         Include("tblTaxon.tblGenus").
                         Include("tblCollector").
                         Include("tblSupplier").
                         ToList());
        }

        public IEnumerable<string> GetAllCodes()
        {
            return _context.tblCollectionItem.Select(x => x.Code).ToList();
        }

        public IEnumerable<CollectionItem> GetBySupplierId(Guid supplierId)
        {
            var entities = _context.tblCollectionItem.Where(x => x.tblSupplierId == supplierId);
            return _translator.ToDomainEntities(entities);
        }

        public IEnumerable<CollectionItem> GetByTaxonId(Guid taxonId)
        {
            var entities = _context.tblCollectionItem.Where(x => x.tblTaxonId == taxonId);
            return _translator.ToDomainEntities(entities);
        }

        public IEnumerable<CollectionItem> GetByCollectorId(Guid collectorId)
        {
            var entities = _context.tblCollectionItem.Where(x => x.tblCollectorId == collectorId);
            return _translator.ToDomainEntities(entities);
        }

        public IEnumerable<CollectionItem> GetByRange(int startIndex, int count)
        {
            var entities = _context.tblCollectionItem.OrderBy(x=>x.Code).Skip(startIndex).Take(count).ToList();
            return _translator.ToDomainEntities(entities);
        }

        public bool ExistsByCode(string code)
        {
            return _context.tblCollectionItem.Any(x => x.Code == code);
        }

        public Guid GetIdByCode(string code)
        {
            return _context.tblCollectionItem.Where(x => x.Code == code).Select(x => x.Id).Single();
        }

        public int GetCount()
        {
            return _context.tblCollectionItem.Count();
        }
    }
}
