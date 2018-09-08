using CactusGuru.Infrastructure.Qualification;
using CactusGuru.Infrastructure.Utils;

namespace CactusGuru.Infrastructure.Persistance
{
    public class SimplePublisher<T> : IPublisher<T>
        where T : DomainEntity
    {
        public SimplePublisher(IRepository<T> repo, ValidatorBase<T> validator)
        {
            _repo = ArgumentChecker.CheckUp(repo);
            _validator = ArgumentChecker.CheckUp(validator);
        }

        private readonly IRepository<T> _repo;
        private readonly ValidatorBase<T> _validator;

        public virtual void Add(T domainEntity)
        {
            _validator.Validate(domainEntity);
            _repo.Add(domainEntity);
        }

        public virtual void Update(T domainEntity)
        {
            _validator.Validate(domainEntity);
            _repo.Update(domainEntity);
        }
    }
}
