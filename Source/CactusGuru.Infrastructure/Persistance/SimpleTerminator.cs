using CactusGuru.Infrastructure.Qualification;
using CactusGuru.Infrastructure.Utils;
using System;

namespace CactusGuru.Infrastructure.Persistance
{
    public class SimpleTerminator<T> : ITerminator<T>
        where T : DomainEntity
    {
        public SimpleTerminator(IUnitOfWork uow, InquiryBase<T> inquiry)
        {
            _uow = ArgumentChecker.CheckUp(uow);
            _inquiry = ArgumentChecker.CheckUp(inquiry);
        }

        private readonly IUnitOfWork _uow;
        private readonly InquiryBase<T> _inquiry;

        public virtual void Terminate(Guid id)
        {
            ArgumentChecker.CheckEmpty(id);
            _inquiry.Inquiry(id);
            _uow.CreateRepository<IRepository<T>>().Delete(id);
        }
    }
}
