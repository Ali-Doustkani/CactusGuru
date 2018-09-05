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
    public class SupplierRepository : ISupplierRepository
    {
        public SupplierRepository(CactusGuruEntities context, TranslatorBase<Supplier, tblSupplier> translator)
        {
            ArgumentChecker.CheckNull(context);
            ArgumentChecker.CheckNull(translator);
            _context = context;
            _translator = translator;
        }

        private readonly CactusGuruEntities _context;
        private readonly TranslatorBase<Supplier, tblSupplier> _translator;

        public void Add(Supplier supplier)
        {
            var entity = _translator.ToDataEntity(supplier);
            _context.tblSupplier.Add(entity);
        }

        public void Update(Supplier supplier)
        {
            var entity = _context.tblSupplier.Find(supplier.Id);
            _translator.FillDataEntity(entity, supplier);
        }

        public void Delete(Guid id)
        {
            var entity = _context.tblSupplier.Find(id);
            _context.tblSupplier.Remove(entity);
        }

        public Supplier Get(Guid id)
        {
            var entity = _context.tblSupplier.Find(id);
            return _translator.ToDomainEntity(entity);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _translator.ToDomainEntities(_context.tblSupplier.ToList());
        }

        public bool HasSimilar(DomainEntity domainEntity)
        {
            var supplier = (Supplier)domainEntity;
            var query = _context.tblSupplier.Where(x => x.Id != supplier.Id);
            if (string.IsNullOrEmpty(supplier.Acronym))
                query = query.Where(x => x.Title == supplier.FullName);
            else
                query = query.Where(x => x.Acronym == supplier.Acronym);
            return query.Any();
        }
    }
}
