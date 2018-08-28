using CactusGuru.Application.Common;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class SupplierViewProvider : CommonDataEntryViewProvider<Supplier, SupplierDto, ISupplierRepository>
    {
        public SupplierViewProvider(IUnitOfWork uow,
           AssemblerBase<Supplier, SupplierDto> assembler,
           IFactory<Supplier> factory,
           IPublisher<Supplier> publisher,
           ITerminator<Supplier> terminator)
       : base(uow, assembler, factory, publisher, terminator) { }

        public override IEnumerable<TransferObjectBase> GetList()
        {
            return base.GetList().Cast<SupplierDto>().OrderBy(x => x.FullName);
        }
    }
}
