using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using System.Collections.Generic;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class CommonDataEntryViewProvider<TDomainEntity, TDtoEntity> : ViewProviderBase, IDataEntryViewProvider
          where TDomainEntity : DomainEntity
          where TDtoEntity : TransferObjectBase, new()
    {
        public virtual IEnumerable<TransferObjectBase> GetList()
        {
            using (var locator = Begin())
            {
                var assembler = locator.Get<AssemblerBase<TDomainEntity, TDtoEntity>>();
                var repo = locator.Get<IRepository<TDomainEntity>>();
                return assembler.ToDataTransferEntities(repo.GetAll());
            }
        }

        public virtual TransferObjectBase Build()
        {
            using (var locator = Begin())
            {
                var factory = locator.Get<IFactory<TDomainEntity>>();
                var assembler = locator.Get<AssemblerBase<TDomainEntity, TDtoEntity>>();
                var domainEntity = factory.CreateNew();
                return assembler.ToDataTransferEntity(domainEntity);
            }
        }

        public virtual TransferObjectBase Copy(TransferObjectBase dto)
        {
            return dto.Clone();
        }

        public virtual void CopyTo(TransferObjectBase source, TransferObjectBase destination)
        {
            new Copier<TDtoEntity>().Copy((TDtoEntity)source, (TDtoEntity)destination);
        }

        public virtual TransferObjectBase Add(TransferObjectBase dto)
        {
            using (var locator = Begin())
            {
                var assembler = locator.Get<AssemblerBase<TDomainEntity, TDtoEntity>>();
                var domainEntity = locator.Get<IFactory<TDomainEntity>>().CreateNew();
                assembler.FillDomainEntity(domainEntity, (TDtoEntity)dto);
                locator.Get<Publisher<TDomainEntity>>().Add(domainEntity);
                locator.Get<IUnitOfWork>().SaveChanges();
                return assembler.ToDataTransferEntity(domainEntity);
            }
        }

        public virtual TransferObjectBase Update(TransferObjectBase dto)
        {
            using (var locator = Begin())
            {
                var assembler = locator.Get<AssemblerBase<TDomainEntity, TDtoEntity>>();
                var domainEntity = locator.Get<IRepository<TDomainEntity>>().Get(((TDtoEntity)dto).Id);
                assembler.FillDomainEntity(domainEntity, (TDtoEntity)dto);
                locator.Get<Publisher<TDomainEntity>>().Update(domainEntity);
                locator.Get<IUnitOfWork>().SaveChanges();
                return assembler.ToDataTransferEntity(domainEntity);
            }
        }

        public virtual void Delete(TransferObjectBase dto)
        {
            using (var locator = Begin())
            {
                var terminator = locator.Get<ITerminator<TDomainEntity>>();
                terminator.Terminate(((TDtoEntity)dto).Id);
            }
        }
    }
}
