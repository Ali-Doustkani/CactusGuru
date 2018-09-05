using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using System.Collections.Generic;
using System.Linq;

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

        public bool HasSimilar(TaxonDto taxon)
        {
            var entity = new Taxon
            {
                Id = taxon.Id,
                Cultivar = taxon.Cultivar,
                Forma = taxon.Forma,
                Genus = new Genus { Id = taxon.Genus.Id },
                Species = taxon.Species,
                SubSpecies = taxon.SubSpecies,
                Variety = taxon.Variety
            };

            return _uow.CreateRepository<ITaxonRepository>().HasSimilar(entity);
        }
    }
}
