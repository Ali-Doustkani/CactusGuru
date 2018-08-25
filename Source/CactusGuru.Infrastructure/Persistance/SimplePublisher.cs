using System;
using CactusGuru.Infrastructure.Qualification;
using CactusGuru.Infrastructure.Utils;

namespace CactusGuru.Infrastructure.Persistance
{
    public class SimplePublisher<T> : IPublisher<T>
        where T : DomainEntity
    {
        public SimplePublisher(IUnitOfWork uow, ValidatorBase<T> validator)
        {
            _uow = ArgumentChecker.CheckUp(uow);
            _validator = ArgumentChecker.CheckUp(validator);
        }

        private readonly IUnitOfWork _uow;
        private readonly ValidatorBase<T> _validator;

        public virtual void Add(T domainEntity)
        {
            _validator.Validate(domainEntity);
            _uow.CreateRepository<IRepository<T>>().Add(domainEntity);
        }

        public virtual void Update(T domainEntity)
        {
            _validator.Validate(domainEntity);
            _uow.CreateRepository<IRepository<T>>().Update(domainEntity);
        }
    }
}
