using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Infrastructure.Persistance
{
    public class Publisher<T> : SimplePublisher<T>
        where T : DomainEntity
    {
        public Publisher(IUnitOfWork uow, ValidatorBase<T> validator)
            : base(uow, validator)
        {
            _uow = uow;
        }

        private readonly IUnitOfWork _uow;

        public override void Add(T domainEntity)
        {
            base.Add(domainEntity);
            _uow.SaveChanges();
        }

        public override void Update(T domainEntity)
        {
            base.Update(domainEntity);
            _uow.SaveChanges();
        }
    }
}
