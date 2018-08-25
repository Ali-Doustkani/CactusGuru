using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Persistance.Repositories
{
    public class GenusRepository : IGenusRepository
    {
        public GenusRepository(CactusGuruEntities context, TranslatorBase<Genus, tblGenus> translator)
        {
            ArgumentChecker.CheckNull(context);
            ArgumentChecker.CheckNull(translator);
            _context = context;
            _translator = translator;
        }

        private readonly CactusGuruEntities _context;
        private readonly TranslatorBase<Genus, tblGenus> _translator;

        public void Add(Genus Genera)
        {
            var entity = _translator.ToDataEntity(Genera);
            _context.tblGenus.Add(entity);
        }

        public void Update(Genus Genera)
        {
            var entity = _context.tblGenus.Find(Genera.Id);
            _translator.FillDataEntity(entity, Genera);
        }

        public void Delete(Guid id)
        {
            var entity = _context.tblGenus.Find(id);
            _context.tblGenus.Remove(entity);
        }

        public Genus Get(Guid id)
        {
            var entity = _context.tblGenus.Find(id);
            return _translator.ToDomainEntity(entity);
        }

        public IEnumerable<Genus> GetAll()
        {
            return _translator.ToDomainEntities(_context.tblGenus);
        }

        public bool HasSimilar(DomainEntity domainEntity)
        {
            var genus = (Genus)domainEntity;
            return _context.tblGenus.Any(x => x.Id != genus.Id && x.Name == genus.Title);
        }
    }
}