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
    public class GenusViewProvider : CommonDataEntryViewProvider<Genus, GenusDto, IGenusRepository>
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
    }
}
