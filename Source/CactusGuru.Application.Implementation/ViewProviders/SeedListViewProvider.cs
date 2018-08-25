using System;
using System.Collections.Generic;
using System.Linq;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Qualification.Validators;
using CactusGuru.Domain.Greenhouse.Sales;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class SeedListViewProvider : ISeedListViewProvider
    {
        public SeedListViewProvider(IUnitOfWork uow)
        {
            ArgumentChecker.CheckNull(uow);
            _uow = uow;
        }

        private readonly IUnitOfWork _uow;

        public SeedList BuildSeedList()
        {
            var ret = new SeedList();
            ret.Id = Guid.NewGuid();
            ret.PublishDate = DateTime.Now;
            return ret;
        }

        public CollectionSeedListItem BuildCollectionSeedListItem(CollectionItem collectionItem)
        {
            var ret = new CollectionSeedListItem(collectionItem);
            ret.Id = Guid.NewGuid();
            return ret;
        }

        public string InquiryForDelete(object domainObject)
        {
            throw new NotImplementedException();
        }

        public void Delete(object domainObject)
        {
            throw new NotImplementedException();
        }

        public object Copy(object domainObject)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(object @from, object to)
        {
            throw new NotImplementedException();
        }

        private void Validate(SeedList seedlist)
        {
            //var validator = new SeedListValidator(seedlist);
            //if (!validator.IsValid())
            //    throw new ErrorHappenedException(validator.ValidationResult());
        }

        public void Add(SeedList seedlist)
        {
            Validate(seedlist);
            _uow.CreateRepository<IRepository<SeedList>>().Add(seedlist);
            AddSeedListItems(seedlist);
            _uow.SaveChanges();
        }

        public void Update(SeedList seedList)
        {
            Validate(seedList);
            _uow.CreateRepository<IRepository<SeedList>>().Update(seedList);
            _uow.CreateRepository<ISeedListItemRepository>().DeleteBySeedListId(seedList.Id);
            AddSeedListItems(seedList);
            _uow.SaveChanges();
        }

        private void AddSeedListItems(SeedList seedList)
        {
            foreach (var item in seedList.Items)
                _uow.CreateRepository<ISeedListItemRepository>().Add(item);
        }

        public IEnumerable<CollectionItem> GetCollectionItems()
        {
            return _uow.CreateRepository<ICollectionItemRepository>().GetAll();
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _uow.CreateRepository<ISupplierRepository>().GetAll();
        }

        public IEnumerable<Taxon> GetTaxa()
        {
            return _uow.CreateRepository<ITaxonRepository>().GetAll();
        }

        public SeedListItemBase BuildSeedListItem(SeedListItemType type, CollectionItem collectionItem, string code, Taxon taxon)
        {
            if (type == SeedListItemType.CollectionItem)
            {
                var ret = new CollectionSeedListItem(collectionItem);
                ret.Id = Guid.NewGuid();
                return ret;
            }
            if (type == SeedListItemType.SupplierItem)
            {
                var ret = new SupplierSeedListItem(code, taxon);
                ret.Id = Guid.NewGuid();
                return ret;
            }
            throw new NotImplementedException();
        }

        public SeedList GetSeedList(Guid id)
        {
            return _uow.CreateRepository<IRepository<SeedList>>().Get(id);
        }

        public IEnumerable<SeedList> GetAllSeedLists()
        {
            return _uow.CreateRepository<IRepository<SeedList>>().GetAll();
        }


        public string GenerateSupplierSeedCode(IEnumerable<SeedListItemBase> items)
        {
            var seedListItems = items.OfType<SupplierSeedListItem>();
            return string.Format("N{0}", seedListItems.Count() + 1);
        }
    }
}
