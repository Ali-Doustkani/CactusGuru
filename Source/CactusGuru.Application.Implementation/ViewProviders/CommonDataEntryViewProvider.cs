using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.Persistance;
using System.Collections.Generic;
using CactusGuru.Application.Common;
using CactusGuru.Infrastructure.ObjectCreation;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class CommonDataEntryViewProvider<TDomainEntity, TDtoEntity, TRepository> : IDataEntryViewProvider
          where TDomainEntity : DomainEntity
          where TDtoEntity : TransferObjectBase, new()
          where TRepository : IRepository<TDomainEntity>
    {
        public CommonDataEntryViewProvider(IUnitOfWork uow,
         AssemblerBase<TDomainEntity, TDtoEntity> assembler,
         IFactory<TDomainEntity> factory,
         IPublisher<TDomainEntity> publisher,
         ITerminator<TDomainEntity> terminator)
        {
            _uow = uow;
            _assembler = assembler;
            _factory = factory;
            _publisher = publisher;
            _terminator = terminator;
        }

        private readonly IUnitOfWork _uow;
        private readonly AssemblerBase<TDomainEntity, TDtoEntity> _assembler;
        private readonly IFactory<TDomainEntity> _factory;
        private readonly IPublisher<TDomainEntity> _publisher;
        private readonly ITerminator<TDomainEntity> _terminator;

        public virtual IEnumerable<TransferObjectBase> GetList()
        {
            return
                _assembler.ToDataTransferEntities(
                    _uow.CreateRepository<TRepository>().GetAll());
        }

        public virtual TransferObjectBase Build()
        {
            var domainEntity = _factory.CreateNew();
            return _assembler.ToDataTransferEntity(domainEntity);
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
            var domainEntity = _factory.CreateNew();
            _assembler.FillDomainEntity(domainEntity, (TDtoEntity)dto);
            _publisher.Add(domainEntity);
            return _assembler.ToDataTransferEntity(domainEntity);
        }

        public virtual TransferObjectBase Update(TransferObjectBase dto)
        {
            var domainEntity = _uow.CreateRepository<TRepository>().Get(((TDtoEntity)dto).Id);
            _assembler.FillDomainEntity(domainEntity, (TDtoEntity)dto);
            _publisher.Update(domainEntity);
            return _assembler.ToDataTransferEntity(domainEntity);
        }

        public virtual void Delete(TransferObjectBase dto)
        {
            _terminator.Terminate(((TDtoEntity)dto).Id);
        }
    }
}
