using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class GenusViewProvider : CommonDataEntryViewProvider<Genus, GenusDto>, IGenusViewProvider
    {
        public override IEnumerable<TransferObjectBase> GetList()
        {
            return base.GetList().Cast<GenusDto>().OrderBy(x => x.Name);
        }

        public bool HasSimilar(GenusDto genusDto)
        {
            var genus = new Genus { Id = genusDto.Id, Title = genusDto.Name };
            return Get<IGenusRepository>().HasSimilar(genus);
        }
    }
}