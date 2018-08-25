using System;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Persistance;
using System.Collections.Generic;
using System.Linq;
using CactusGuru.Application.Common;
using CactusGuru.Infrastructure.ObjectCreation;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class CollectionItemViewProvider : CommonDataEntryViewProvider<CollectionItem, CollectionItemDto, ICollectionItemRepository>, ICollectionItemViewProvider
    {
        public CollectionItemViewProvider(IUnitOfWork uow,
            AssemblerBase<CollectionItem, CollectionItemDto> assembler,
            IFactory<CollectionItem> factory,
            IPublisher<CollectionItem> publisher,
            ITerminator<CollectionItem> terminator,
            AssemblerBase<Taxon, TaxonDto> taxonAssembler,
            AssemblerBase<Collector, CollectorDto> collectorAssembler,
            AssemblerBase<Supplier, SupplierDto> supplierAssembler,
            AssemblerBase<CollectionItem, CollectionItemDto> collectionItemAssembler)
            : base(uow, assembler, factory, publisher, terminator)
        {
            _uow = uow;
            _taxonAssembler = taxonAssembler;
            _collectorAssembler = collectorAssembler;
            _supplierAssembler = supplierAssembler;
            _collectionItemAssembler = collectionItemAssembler;
        }

        private readonly IUnitOfWork _uow;
        private readonly AssemblerBase<Taxon, TaxonDto> _taxonAssembler;
        private readonly AssemblerBase<Collector, CollectorDto> _collectorAssembler;
        private readonly AssemblerBase<Supplier, SupplierDto> _supplierAssembler;
        private readonly AssemblerBase<CollectionItem, CollectionItemDto> _collectionItemAssembler;

        public CollectionItemDto GetCollectionItem(Guid id)
        {
            var item = _uow.CreateRepository<ICollectionItemRepository>().Get(id);
            return _collectionItemAssembler.ToDataTransferEntity(item);
        }

        public IEnumerable<TaxonDto> GetTaxa()
        {
            var taxa = _uow.CreateRepository<ITaxonRepository>().GetAll().OrderBy(x => x.Genus.Title);
            return _taxonAssembler.ToDataTransferEntities(taxa);
        }

        public IEnumerable<CollectorDto> GetCollectors()
        {
            var collectors = _uow.CreateRepository<ICollectorRepository>().GetAll().OrderBy(x => x.FullName);
            return _collectorAssembler.ToDataTransferEntities(collectors);
        }

        public IEnumerable<SupplierDto> GetSuppliers()
        {
            var suppliers = _uow.CreateRepository<ISupplierRepository>().GetAll().OrderBy(x => x.FullName);
            return _supplierAssembler.ToDataTransferEntities(suppliers);
        }

        public IEnumerable<IncomeTypeDto> GetIncomeTypes()
        {
            var ret = new List<IncomeTypeDto>();
            ret.Add(new IncomeTypeDto { Value = (int)IncomeType.Plant });
            ret.Add(new IncomeTypeDto { Value = (int)IncomeType.Seed });
            return ret;
        }
    }
}
