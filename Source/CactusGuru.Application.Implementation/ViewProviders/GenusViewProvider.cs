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
    public class GenusViewProvider : CommonDataEntryViewProvider<Genus, GenusDto, IGenusRepository>, IGenusViewProvider
    {
        public GenusViewProvider(IUnitOfWork uow,
            AssemblerBase<Genus, GenusDto> assembler,
            IFactory<Genus> factory,
            IPublisher<Genus> publisher,
            ITerminator<Genus> terminator)
       : base(uow, assembler, factory, publisher, terminator) { }

        public override IEnumerable<TransferObjectBase> GetList()
        {
            return base.GetList().Cast<GenusDto>().OrderBy(x => x.Name);
        }

        public bool HasSimilar(GenusDto genusDto)
        {
            var genus = new Genus { Id = genusDto.Id, Title = genusDto.Name };
            return unitOfWork.CreateRepository<IGenusRepository>().HasSimilar(genus);
        }
    }
}
