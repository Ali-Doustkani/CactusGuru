using CactusGuru.Domain.Greenhouse.Sales;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CactusGuru.Persistance.Repositories
{
    public class SeedListRepository : IRepository<SeedList>
    {
        public SeedListRepository(CactusGuruEntities context, TranslatorBase<SeedList, tblSeedList> translator)
        {
            ArgumentChecker.CheckNull(context);
            ArgumentChecker.CheckNull(translator);
            _context = context;
            _translator = translator;
        }

        private readonly CactusGuruEntities _context;
        private readonly TranslatorBase<SeedList, tblSeedList> _translator;

        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void Add(SeedList seedList)
        {
            ArgumentChecker.CheckEmpty(seedList);
            var entity = _translator.ToDataEntity(seedList);
            _context.tblSeedList.Add(entity);
        }

        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public void Update(SeedList seedList)
        {
            ArgumentChecker.CheckEmpty(seedList);
            var entity = _context.tblSeedList.Find(seedList.Id);
            _translator.FillDataEntity(entity, seedList);
        }

        /// <exception cref="ArgumentException"/>
        public void Delete(Guid id)
        {
            ArgumentChecker.CheckEmpty(id);
            var entity = _context.tblSeedList.Find(id);
            _context.tblSeedList.Remove(entity);
        }

        /// <exception cref="ArgumentException"/>
        public SeedList Get(Guid id)
        {
            ArgumentChecker.CheckEmpty(id);
            var entity = _context.tblSeedList.Find(id);
            return _translator.ToDomainEntity(entity);
        }

        public IEnumerable<SeedList> GetAll()
        {
            return _translator.ToDomainEntities(DefaultQuery());
        }

        private IQueryable<tblSeedList> DefaultQuery()
        {
            return _context.tblSeedList.Include(x => x.tblSeedListItem)
                .Include("SeedListItems.CollectionItems")
                .Include("SeedListItems.Suppliers")
                .Include("SeedListItems.Taxa")
                .Include("SeedListItems.Taxa.Genera");
        }
    }
}
