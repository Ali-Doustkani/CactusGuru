using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Domain.Greenhouse;
using System.Collections.Generic;
using CactusGuru.Domain.Persistance.Repositories;
using System;

namespace CactusGuru.Application.Implementation.ViewProviders.LabelPrinting
{
    public class LabelPrintViewProvider : ILabelPrintViewProvider
    {
        public LabelPrintViewProvider(ICollectionItemRepository itemRepo,
            ITaxonRepository taxonRepo,
            AssemblerBase<CollectionItem, CollectionItemDto> itemAssembler,
            AssemblerBase<Taxon, TaxonDto> taxonAssembler)
        {
            _itemRepo = itemRepo;
            _taxonRepo = taxonRepo;
            _itemAssembler = itemAssembler;
            _taxonAssembler = taxonAssembler;
        }

        private readonly ICollectionItemRepository _itemRepo;
        private readonly ITaxonRepository _taxonRepo;
        private readonly AssemblerBase<CollectionItem, CollectionItemDto> _itemAssembler;
        private readonly AssemblerBase<Taxon, TaxonDto> _taxonAssembler;

        public CollectionItemDto GetCollectionItem(Guid id)
        {
            var item = _itemRepo.Get(id);
            return _itemAssembler.ToDataTransferEntity(item);
        }

        public IEnumerable<CollectionItemDto> GetCollectionItems()
        {
            var items = _itemRepo.GetAll();
            return _itemAssembler.ToDataTransferEntities(items);
        }

        public TaxonDto GetTaxon(Guid id)
        {
            var item = _taxonRepo.Get(id);
            return _taxonAssembler.ToDataTransferEntity(item);
        }

        public IEnumerable<TaxonDto> GetTaxa()
        {
            var items = _taxonRepo.GetAll();
            return _taxonAssembler.ToDataTransferEntities(items);
        }
    }
}
