using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Persistance;
using System.Collections.Generic;
using System.Linq;
using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.ObjectCreation;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class TaxonViewProvider : CommonDataEntryViewProvider<Taxon, TaxonDto, ITaxonRepository>, ITaxonViewProvider
    {
        public TaxonViewProvider(IUnitOfWork uow,
            AssemblerBase<Taxon, TaxonDto> taxonAssembler,
            AssemblerBase<Genus, GenusDto> genusAssembler,
            IFactory<Taxon> factory,
            IPublisher<Taxon> publisher,
            ITerminator<Taxon> terminator)
            : base(uow, taxonAssembler, factory, publisher, terminator)
        {
            _uow = uow;
            _genusAssembler = genusAssembler;
        }

        private readonly IUnitOfWork _uow;
        private readonly AssemblerBase<Genus, GenusDto> _genusAssembler;

        public override IEnumerable<TransferObjectBase> GetList()
        {
            return base.GetList().Cast<TaxonDto>().OrderBy(x => x.Genus.Name);
        }

        public IEnumerable<GenusDto> GetGenera()
        {
            return
                _genusAssembler.ToDataTransferEntities(
                    _uow.CreateRepository<IGenusRepository>().GetAll().OrderBy(x => x.Title));
        }
    }
}
