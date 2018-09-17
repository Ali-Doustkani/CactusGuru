using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class GenusViewProvider : CommonDataEntryViewProvider<Genus, GenusDto>, IGenusViewProvider
    {
        public bool HasSimilar(GenusDto genusDto)
        {
            using (var locator = Begin())
            {
                var genus = new Genus { Id = genusDto.Id, Title = genusDto.Name };
                return locator.Get<IGenusRepository>().HasSimilar(genus);
            }
        }

        protected override object GetOrderKey(GenusDto dto) => dto.Name;
    }
}