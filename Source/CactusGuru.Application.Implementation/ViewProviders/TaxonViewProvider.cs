using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class TaxonViewProvider : CommonDataEntryViewProvider<Taxon, TaxonDto>, ITaxonViewProvider
    {
        public override IEnumerable<TransferObjectBase> GetList()
        {
            return base.GetList().Cast<TaxonDto>().OrderBy(x => x.Genus.Name);
        }

        public IEnumerable<GenusDto> GetGenera()
        {
            var assembler = Get<AssemblerBase<Genus, GenusDto>>();
            var repo = Get<IGenusRepository>();
            return assembler.ToDataTransferEntities(repo.GetAll().OrderBy(x => x.Title));
        }

        public bool HasSimilar(TaxonDto taxon)
        {
            if (taxon.Genus == null)
                return false;

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

            return Get<ITaxonRepository>().HasSimilar(entity);
        }
    }
}
