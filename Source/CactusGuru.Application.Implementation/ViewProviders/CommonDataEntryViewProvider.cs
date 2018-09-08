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
            var assembler = Get<AssemblerBase<TDomainEntity, TDtoEntity>>();
            var repo = Get<IRepository<TDomainEntity>>();
            return assembler.ToDataTransferEntities(repo.GetAll());
        }

        public virtual TransferObjectBase Build()
        {
            var factory = Get<IFactory<TDomainEntity>>();
            var assembler = Get<AssemblerBase<TDomainEntity, TDtoEntity>>();
            var domainEntity = factory.CreateNew();
            return assembler.ToDataTransferEntity(domainEntity);
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
            var factory = Get<IFactory<TDomainEntity>>();
            var assembler = Get<AssemblerBase<TDomainEntity, TDtoEntity>>();
            var publisher = Get<IPublisher<TDomainEntity>>();

            var domainEntity = factory.CreateNew();
            assembler.FillDomainEntity(domainEntity, (TDtoEntity)dto);
            publisher.Add(domainEntity);
            return assembler.ToDataTransferEntity(domainEntity);
        }

        public virtual TransferObjectBase Update(TransferObjectBase dto)
        {
            var repo = Get<IRepository<TDomainEntity>>();
            var factory = Get<IFactory<TDomainEntity>>();
            var assembler = Get<AssemblerBase<TDomainEntity, TDtoEntity>>();
            var publisher = Get<IPublisher<TDomainEntity>>();

            var domainEntity = repo.Get(((TDtoEntity)dto).Id);
            assembler.FillDomainEntity(domainEntity, (TDtoEntity)dto);
            publisher.Update(domainEntity);
            return assembler.ToDataTransferEntity(domainEntity);
        }

        public virtual void Delete(TransferObjectBase dto)
        {
            var terminator = Get<ITerminator<TDomainEntity>>();
            terminator.Terminate(((TDtoEntity)dto).Id);
        }
    }
}
