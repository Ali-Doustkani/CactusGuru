using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Repositories;
using StructureMap;
using System;
using System.Collections.Generic;

namespace CactusGuru.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(CactusGuruEntities context, IContext container)
        {
            ArgumentChecker.CheckNull(context);
            ArgumentChecker.CheckNull(container);
            _context = context;
            _container = container;
            _dic = new Dictionary<Type, Func<object>>();
            InitializeTypes();
        }

        private readonly CactusGuruEntities _context;
        private readonly IContext _container;
        private readonly Dictionary<Type, Func<object>> _dic;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public T CreateRepository<T>()
        {
            var requestedType = typeof(T);
            if (!_dic.ContainsKey(requestedType))
                throw new NotImplementedException("Repository is not registered in the unit of work.");

            return (T)_dic[requestedType].Invoke();
        }

        private void InitializeTypes()
        {
            _dic.Add(typeof(IGenusRepository), GenusRepo);
            _dic.Add(typeof(IRepository<Genus>), GenusRepo);
            _dic.Add(typeof(ITaxonRepository), TaxonRepo);
            _dic.Add(typeof(IRepository<Taxon>), TaxonRepo);
            _dic.Add(typeof(ISupplierRepository), SupplierRepo);
            _dic.Add(typeof(IRepository<Supplier>), SupplierRepo);
            _dic.Add(typeof(IRepository<Collector>), CollectorRepo);
            _dic.Add(typeof(ICollectorRepository), CollectorRepo);
            _dic.Add(typeof(IRepository<CollectionItem>), CollectionItemRepo);
            _dic.Add(typeof(ICollectionItemRepository), CollectionItemRepo);
            _dic.Add(typeof(IRepository<CollectionItemImage>), CollectionItemImageRepo);
            _dic.Add(typeof(ICollectionItemImageRepository), CollectionItemImageRepo);
        }

        private object GenusRepo()
        {
            return new GenusRepository(_context,_container.GetInstance<TranslatorBase<Genus, tblGenus>>());
        }

        private object TaxonRepo()
        {
            return new TaxonRepository(_context, _container.GetInstance<TranslatorBase<Taxon, tblTaxon>>());
        }

        private object SupplierRepo()
        {
            return new SupplierRepository(_context, _container.GetInstance<TranslatorBase<Supplier, tblSupplier>>());
        }

        private object CollectorRepo()
        {
            return new CollectorRepository(_context, _container.GetInstance<TranslatorBase<Collector, tblCollector>>());
        }

        private object CollectionItemRepo()
        {
            return new CollectionItemRepository(_context, _container.GetInstance<TranslatorBase<CollectionItem, tblCollectionItem>>());
        }

        private object CollectionItemImageRepo()
        {
            return new CollectionItemImageRepository(_context, _container.GetInstance<TranslatorBase<CollectionItemImage, tblCollectionItemImage>>());
        }
    }
}
