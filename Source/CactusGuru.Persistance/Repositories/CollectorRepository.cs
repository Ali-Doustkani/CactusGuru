using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Persistance.Repositories
{
    public class CollectorRepository : ICollectorRepository
    {
        public CollectorRepository(CactusGuruEntities context, TranslatorBase<Collector, tblCollector> translator)
        {
            _context = context;
            _translator = translator;
        }

        private readonly CactusGuruEntities _context;
        private readonly TranslatorBase<Collector, tblCollector> _translator;

        public void Add(Collector collector)
        {
            var entity = _translator.ToDataEntity(collector);
            _context.tblCollector.Add(entity);
        }

        public void Update(Collector collector)
        {
            var entity = _context.tblCollector.Find(collector.Id);
            _translator.FillDataEntity(entity, collector);
        }

        public void Delete(Guid id)
        {
            var collector = _context.tblCollector.Find(id);
            _context.tblCollector.Remove(collector);
        }

        public Collector Get(Guid id)
        {
            var entity = _context.tblCollector.Find(id);
            return _translator.ToDomainEntity(entity);
        }

        public IEnumerable<Collector> GetAll()
        {
            return _translator.ToDomainEntities(_context.tblCollector);
        }

        public bool HasSimilar(DomainEntity domainEntity)
        {
            var collector = (Collector)domainEntity;
            return _context.tblCollector.Any(x => x.Id != collector.Id && (x.FieldAcronym == collector.Acronym || x.Title == collector.FullName));
        }
    }
}
