using CactusGuru.Domain.Greenhouse.Sales;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CactusGuru.Domain.Persistance.Repositories;

namespace CactusGuru.Persistance.Repositories
{
    public class SeedListItemRepository : ISeedListItemRepository
    {
        public SeedListItemRepository(CactusGuruEntities context, TranslatorBase<SeedListItemBase, tblSeedListItem> translator)
        {
            ArgumentChecker.CheckNull(context);
            ArgumentChecker.CheckNull(translator);
            _context = context;
            _translator = translator;
        }

        private readonly CactusGuruEntities _context;
        private readonly TranslatorBase<SeedListItemBase, tblSeedListItem> _translator;

        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void Add(SeedListItemBase seedListItem)
        {
            ArgumentChecker.CheckEmpty(seedListItem);
            var entity = _translator.ToDataEntity(seedListItem);
            _context.tblSeedListItem.Add(entity);
        }

        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void Update(SeedListItemBase seedListItem)
        {
            ArgumentChecker.CheckEmpty(seedListItem);
            var entity = _context.tblSeedListItem.Find(seedListItem.Id);
            _translator.FillDataEntity(entity, seedListItem);
        }

        /// <exception cref="ArgumentException"/>
        public void Delete(Guid id)
        {
            ArgumentChecker.CheckEmpty(id);
            var entity = _context.tblSeedListItem.Find(id);
            _context.tblSeedListItem.Remove(entity);
        }

        public void DeleteBySeedListId(Guid id)
        {
            ArgumentChecker.CheckEmpty(id);
            var entities = _context.tblSeedListItem.Where(x => x.tblSeedListId == id);
            foreach (var entity in entities)
                _context.tblSeedListItem.Remove(entity);
        }

        /// <exception cref="ArgumentException"/>
        public SeedListItemBase Get(Guid id)
        {
            ArgumentChecker.CheckEmpty(id);
            var entity = DefaultQuery().Single(x => x.Id == id);
            return _translator.ToDomainEntity(entity);
        }

        public IEnumerable<SeedListItemBase> GetAll()
        {
            return _translator.ToDomainEntities(DefaultQuery());
        }

        private IQueryable<tblSeedListItem> DefaultQuery()
        {
            return _context.tblSeedListItem.Include(x => x.tblCollectionItem)
                .Include(x => x.tblSupplier)
                .Include(x => x.tblTaxon);
        }
    }
}
