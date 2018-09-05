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
    public class CollectorViewProvider : CommonDataEntryViewProvider<Collector, CollectorDto, IRepository<Collector>>, ICollectorViewProvider
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

        public bool HasSimilar(CollectorDto dto)
        {
            var collector = new Collector
            {
                Acronym = dto.Acronym,
                FullName = dto.FullName,
                Id = dto.Id
            };
            return unitOfWork.CreateRepository<ICollectorRepository>().HasSimilar(collector);
        }
    }
}
