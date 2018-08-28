using CactusGuru.Application.Common;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class CollectorViewProvider : CommonDataEntryViewProvider<Collector, CollectorDto, IRepository<Collector>>
    {
        public CollectorViewProvider(IUnitOfWork uow,
          AssemblerBase<Collector, CollectorDto> assembler,
          IFactory<Collector> factory,
          IPublisher<Collector> publisher,
          ITerminator<Collector> terminator)
       : base(uow, assembler, factory, publisher, terminator) { }

        public override IEnumerable<TransferObjectBase> GetList()
        {
            return base.GetList().Cast<CollectorDto>().OrderBy(x => x.FullName);
        }
    }
}
