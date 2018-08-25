using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;

namespace CactusGuru.Persistance.Repositories
{
    public class TaxonRepository : ITaxonRepository
    {
        public TaxonRepository(CactusGuruEntities context, TranslatorBase<Taxon, tblTaxon> translator)
        {
            ArgumentChecker.CheckNull(context);
            ArgumentChecker.CheckNull(translator);
            _context = context;
            _translator = translator;
        }

        private readonly CactusGuruEntities _context;
        private readonly TranslatorBase<Taxon, tblTaxon> _translator;

        public void Add(Taxon taxon)
        {
            var entity = _translator.ToDataEntity(taxon);
            _context.tblTaxon.Add(entity);
        }

        public void Update(Taxon taxon)
        {
            var entity = _context.tblTaxon.Find(taxon.Id);
            _translator.FillDataEntity(entity, taxon);
        }

        public void Delete(Guid id)
        {
            var entity = _context.tblTaxon.Find(id);
            _context.tblTaxon.Remove(entity);
        }

        public Taxon Get(Guid id)
        {
            var entity = _context.tblTaxon.Find(id);
            return _translator.ToDomainEntity(entity);
        }

        public IEnumerable<Taxon> GetAll()
        {
            return _translator.ToDomainEntities(DefaultQuery());
        }

        public IEnumerable<Taxon> GetByGeneraId(Guid guid)
        {
            var entities = DefaultQuery().Where(x => x.tblGenusId == guid);
            return _translator.ToDomainEntities(entities);
        }

        public bool HasSimilar(DomainEntity domainEntity)
        {
            var taxon = (Taxon)domainEntity;
            return _context.tblTaxon.Any(x => x.Id != taxon.Id &&
            x.Species == taxon.Species &&
            x.SubSpecies == taxon.SubSpecies &&
            x.tblGenusId == taxon.Genus.Id &&
            x.Variety == taxon.Variety);
        }

        private IQueryable<tblTaxon> DefaultQuery()
        {
            return _context.tblTaxon.Include(x => x.tblGenus);
        }
    }
}
