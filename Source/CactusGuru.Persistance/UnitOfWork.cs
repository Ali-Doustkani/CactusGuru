﻿using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Sales;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;
using CactusGuru.Persistance.Repositories;
using System;
using System.Collections.Generic;

namespace CactusGuru.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(CactusGuruEntities context, IResolver resolver)
        {
            ArgumentChecker.CheckNull(context);
            ArgumentChecker.CheckNull(resolver);
            _context = context;
            _translatorResolver = resolver;
            _dic = new Dictionary<Type, Func<object>>();
            InitializeTypes();
        }

        private readonly CactusGuruEntities _context;
        private readonly IResolver _translatorResolver;
        private readonly Dictionary<Type, Func<object>> _dic;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public T CreateRepository<T>()
        {
            if (typeof(T) == typeof(ISeedListItemRepository))
                return (T)(object)new SeedListItemRepository(_context, _translatorResolver.Resolve<TranslatorBase<SeedListItemBase, tblSeedListItem>>());
            if (typeof(T) == typeof(IRepository<SeedList>))
                return (T)(object)new SeedListRepository(_context, _translatorResolver.Resolve<TranslatorBase<SeedList, tblSeedList>>());

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
            return new GenusRepository(_context, _translatorResolver.Resolve<TranslatorBase<Genus, tblGenus>>());
        }

        private object TaxonRepo()
        {
            return new TaxonRepository(_context, _translatorResolver.Resolve<TranslatorBase<Taxon, tblTaxon>>());
        }

        private object SupplierRepo()
        {
            return new SupplierRepository(_context, _translatorResolver.Resolve<TranslatorBase<Supplier, tblSupplier>>());
        }

        private object CollectorRepo()
        {
            return new CollectorRepository(_context, _translatorResolver.Resolve<TranslatorBase<Collector, tblCollector>>());
        }

        private object CollectionItemRepo()
        {
            return new CollectionItemRepository(_context, _translatorResolver.Resolve<TranslatorBase<CollectionItem, tblCollectionItem>>());
        }

        private object CollectionItemImageRepo()
        {
            return new CollectionItemImageRepository(_context, _translatorResolver.Resolve<TranslatorBase<CollectionItemImage, tblCollectionItemImage>>());
        }
    }
}
